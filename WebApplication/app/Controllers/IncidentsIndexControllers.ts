
class IncidentsIndexController extends BaseIndexController {

    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("Incidents", $scope, $uibModal, database);
        this.setupController();
        this.RefreshGrid();
    }

    public setupController() {
		this.Display.IncidentType = true;
		this.Display.IncidentDate = true;
		this.Display.IncidentTime = true;
		this.Display.Description = true;
		this.Display.Person = true;
		this.Display.Id = true;
    }

    public beforeRefreshGrid() {
    }
}


myApp.controller("IncidentsIndexController", IncidentsIndexController);

