
class IncidentsDetailsController extends BaseDetailsController {

    public static $inject = BaseDetailsController.$inject.concat(["$http", "sheAlert"]);
    $onInit() { }
    public currentAccidentCar;
    public cars: Array<{ Id, Name }> = [];
    public incidentTypes: Array<{ Id, Name }> = [];
    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId, private $http: ng.IHttpService, private sheAlert: SheAlert) {
        super("Incidents", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
        this.model.IncidentDate = new Date(this.model.IncidentDate);
        this.$uibModalInstance.rendered.then(r => {
        });
    }

    protected afterLoad() {
        //this.GetIncidentTypes();
        if (this.model.IncidentDate != null) {
            this.model.IncidentDate = new Date(this.model.IncidentDate);
        }
        if (this.model.IncidentTime != null) {
            this.model.IncidentTime = new Date(this.model.IncidentTime);
        }
    }
}


myApp.controller("IncidentsDetailsController", IncidentsDetailsController);

