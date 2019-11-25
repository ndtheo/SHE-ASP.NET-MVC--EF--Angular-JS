
class Database {

    public static $inject = ["$http", "sheAlert", "$window", "$timeout"];

    constructor(
        private readonly $http: ng.IHttpService,
        private readonly sheAlert: SheAlert,
        private readonly $window: ng.IWindowService,
        private readonly $timeout: ng.ITimeoutService
    ) { }

    public Save(controllerName: string, model, successCallback?: Function, errorCallback?: Function, extraParams = ""): void {

        const saveModel: any = this.cleanObjectForSave(model);

        if (extraParams) extraParams = "?" + extraParams;

        if (typeof (saveModel.Id) == "undefined" || saveModel.Id === 0) {
            this.saveAsNew(controllerName, saveModel, successCallback, errorCallback, extraParams);
        } else {
            this.saveAsExisting(controllerName, saveModel, successCallback, errorCallback, extraParams);
        }
    }

    private saveAsNew(controllerName: string, saveModel: any, successCallback?: Function, errorCallback?: Function, extraParams="") {
        this.$http.post(`../Api/${controllerName}${extraParams}`, saveModel)
            .then(response => {
                    this.sheAlert.success("Data Saved");
                    this.Load(controllerName, (response.data as any).Id, successCallback, errorCallback);
                },
                response => {
                    if (response.data.length > 0)
                        this.sheAlert.error(response.data);
                    else
                        this.sheAlert.error("There was an error saving data");

                    if (errorCallback != null) {
                        errorCallback(response);
                    }
                });
    }

    private saveAsExisting(controllerName: string, saveModel: any, successCallback?: Function, errorCallback?: Function, extraParams="") {
        this.$http.put<any>(`../Api/${controllerName}/${saveModel.Id}${extraParams}`, saveModel)
            .then(response => {
                    this.sheAlert.success("Data Saved");
                    if (typeof (response.data.Id) != "undefined" && response.data.Id != null) {
                        this.Load(controllerName, response.data.Id, successCallback, errorCallback);
                    } else {
                        this.Load(controllerName, saveModel.Id, successCallback, errorCallback);
                    }
                },
                response => {
                    if (response.data.length > 0)
                        this.sheAlert.error(response.data);
                    else
                        this.sheAlert.error("There was an error saving data");

                    if (errorCallback != null) {
                        errorCallback(response);
                    }
                });
	}

    public Load(controllerName: string, id, successCallback?: Function, errorCallback?: Function): void {
        this.$http.get(`../api/${controllerName}/${id}`)
            .then(response => {
                response.data = this.cleanUpResponseData(response.data);
                if (successCallback != null) {
                    successCallback(response.data);
                }
            },
            response => {
                if (response.data.length > 0)
                    this.sheAlert.error(response.data);
                else
                    this.sheAlert.error("There was an error loading data");

                if (errorCallback != null) {
                    errorCallback(response);
                }
            });
    };

    public Delete(controllerName: string, id, successCallback?: Function, errorCallback?: Function): void {

        this.$http.delete(`../api/${controllerName}/${id}`)
            .then(response => {
                this.sheAlert.success("Data Deleted");
                if (successCallback != null) {
                    successCallback(response);
                }
            },
            response => {
                if (response.data.length > 0)
                    this.sheAlert.error(response.data);
                else
                    this.sheAlert.error("There was an error deleting data");

                if (errorCallback != null) {
                    errorCallback(response);
                }
            });
    };

    public Search(controllerName: string, searchCriteria, successCallback?: Function, errorCallback?: Function): void {

        this.$http.post(`../${controllerName}/Search`, searchCriteria)
            .then(response => {
                if (successCallback != null) {
                    successCallback(response);
                }
            },
            response => {
                this.sheAlert.error("There was an error loading data");

                if (errorCallback != null) {
                    errorCallback(response);
                }
            });
    };

    public ExportGridToExcel(controllerName: string, data, display, successCallback?: Function, errorCallback?: Function): void {

        const postdata = { Display: display, SearchCriteria: data };

        this.$http.post<string>(`../${controllerName}/ExportGridToExcel`, postdata)
            .then(response => {
                this.sheAlert.success("Data Exported");
                    this.$window.location.href = response.data;
                    if (successCallback != null) {
                        successCallback(response);
                    }
                },
                response => {
                    this.sheAlert.error("There was an error exporting data");

                    if (errorCallback != null) {
                        errorCallback(response);
                    }
                });
	};

    public CheckNameExists(controllerName: string, model, successCallback?: Function): void {

        this.$http.get(`../${controllerName}/CheckNameExists?name=${model.Name}&id=${model.Id}`)
            .then(response => {
                    if (successCallback != null) {
                        const isValid = response.data != "True";
                        successCallback(isValid);
                    }
                },
                response => {
                    this.sheAlert.error("Could not check if the name is unique");
                });
    };

    public CheckNameExistsInParent(controllerName: string, model, parentId, successCallback?: Function): void {

        if (parentId > 0) {
            this.$http.get(
                    `../${controllerName}/CheckNameExists?name=${model.Name}&id=${model.Id}&parentId=${parentId}`)
                .then(response => {
                        if (successCallback != null) {
                            const isValid = response.data != "True";
                            successCallback(isValid);
                        }
                    },
                    response => {
                        this.sheAlert.error("Could not check if the name is unique");
                    });
        }
    }

    private cleanUpResponseData(data) {
        data.CreationDate = this.parseDate(data.CreationDate);
        data.LastUpdateDate = this.parseDate(data.LastUpdateDate);
		data.AccidentDate = this.parseDate(data.AccidentDate);
		data.AssignmentDate = this.parseDate(data.AssignmentDate);
		data.ReleaseDate = this.parseDate(data.ReleaseDate);
		data.ManufacturingDate = this.parseDate(data.ManufacturingDate);
		data.AutopsyDate1 = this.parseDate(data.AutopsyDate1);
		data.AutopsyDate2 = this.parseDate(data.AutopsyDate2);
		data.AutopsyDate3 = this.parseDate(data.AutopsyDate3);
		data.ReportDate = this.parseDate(data.ReportDate);
		data.DateLogin = this.parseDate(data.DateLogin);
		data.DateLogout = this.parseDate(data.DateLogout);
        data.ReleaseDateInternational = this.parseDate(data.ReleaseDateInternational);
		if(data.hasOwnProperty('Accident')){
			data.Accident.AccidentDate = this.parseDate(data.Accident.AccidentDate);
			data.Accident.AssignmentDate = this.parseDate(data.Accident.AssignmentDate);
		}
        if (data.hasOwnProperty('Car') && data.Car!=null) {
			data.Car.ReleaseDate = this.parseDate(data.Car.ReleaseDate);
			data.Car.ManufacturingDate = this.parseDate(data.Car.ManufacturingDate);
			data.Car.ReleaseDateInternational = this.parseDate(data.Car.ReleaseDateInternational);
		}
        return data;
    };

    // Drop the hour from the date
    private cleanObjectForSave(model) {
        const data = angular.copy(model);
        for (let prop in data) {
            if (data.hasOwnProperty(prop) && data[prop] instanceof Date && prop !== "CreationDate") {
                data[prop] = moment(data[prop]).format("YYYY-MM-DD");
            }
        }
        return data;
    }

    private parseDate(date) {

        if (typeof (date) == "undefined" || date == null)
            return null;

        try {
        const serverDate = new Date(date);
        return serverDate;
    }
        catch (er) {
            console.warn(er.message);
            return date;
        }
    }
}

myApp.service("database", Database);
