
class IncidentsDetailsController extends BaseDetailsController {

    public static $inject = BaseDetailsController.$inject.concat(["$http", "sheAlert"]);
    $onInit() { }
    public currentAccidentCar;
    public cars: Array<{ Id, Name }> = [];
    public experts: Array<{ Id, Name }> = [];
    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId, private $http: ng.IHttpService, private sheAlert: SheAlert) {
        super("Incidents", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
        this.model.IncidentDate = new Date(this.model.IncidentDate);
        this.$uibModalInstance.rendered.then(r => {
            this.GetIncidentTypes();
        });
    }

    public GetIncidentTypes() {
        if (!this.model.Id) {
            this.experts = [];
            return;
        }

        this.$http.get<Array<{ Id, Name }>>(`../Incidents/GetIncidentTypes`)
            .then(response => {
                this.experts = response.data;
            },
                response => {
                    this.sheAlert.error("There was an error loading cars");
                });
	}

    protected afterLoad() {
        this.GetIncidentTypes();
        if (this.model.IncidentDate != null) {
            this.model.IncidentDate = new Date(this.model.IncidentDate);
        }
        if (this.model.IncidentTime != null) {
            this.model.IncidentTime = new Date(this.model.IncidentTime);
        }
    }
}


myApp.controller("IncidentsDetailsController", IncidentsDetailsController);

