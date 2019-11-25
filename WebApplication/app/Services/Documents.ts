
class Documents {

    public static $inject = ["$http", "$uibModal", "$window", "visionAlert"];

    constructor(private readonly $http: ng.IHttpService, private readonly $uibModal: ng.ui.bootstrap.IModalService, private readonly $window:ng.IWindowService, private readonly visionAlert:SheAlert) {}

    public uploadFileToUrl(file, uploadUrl, successCallback, errorCallback) {
        const formData = new FormData();
        formData.append("file", file);

        this.$http.post(uploadUrl, formData, { transformRequest: angular.identity, headers: { 'Content-Type': undefined } })
            .then(response => { successCallback(response.data); }, errorCallback);
    }

	public openFileUploader = (successCallback) => {
		let url = "../Documents/Uploader";
        var modalInstance = this.$uibModal.open({
            animation: true,
            templateUrl: url,
            backdrop: "static",
            controller: "DocumentUploadController",
            controllerAs: "ctrl",
            size: "sm"
        });

        modalInstance.result.then(successCallback);
    };


    public deleteDocument(id, successCallback) {
        this.$http.get(`../Documents/Delete/${id}`)
            .then(response => {
                this.visionAlert.success("File Deleted");
                successCallback();
            },
            response => {
                this.visionAlert.error("There was an error deleting file");
            });
    };
}

myApp.service("documents", Documents);