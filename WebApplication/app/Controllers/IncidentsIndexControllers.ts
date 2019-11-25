
class IncidentsIndexController extends BaseIndexController {

    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("Incidents", $scope, $uibModal, database);
        this.setupController();
        this.RefreshGrid();
    }

    public setupController() {
		this.Display.AccidentType = true;
		this.Display.Creator = true;
		this.Display.LastUpdateUser = true;
		this.Display.AccidentDate = true;
		this.Display.InsuranceNo = true;
		this.Display.Location = true;
		this.Display.OrderNo = true;
		this.Display.AssignmentDate = true;
		this.Display.DamageNo = true;
		this.Display.Comments = true;
		this.Display.CreationDate = true;
		this.Display.LastUpdateDate = true;
	
    }

    public beforeRefreshGrid() {
    }
}


myApp.controller("IncidentsIndexController", IncidentsIndexController);

