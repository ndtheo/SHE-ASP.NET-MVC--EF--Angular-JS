
class AccidentTypesIndexController extends BaseIndexController {

    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("AccidentTypes", $scope, $uibModal, database);
        this.setupController();
        this.RefreshGrid();
    }

    public setupController() {
		this.Display.Creator = true;
		this.Display.LastUpdateUser = true;
		this.Display.Name = true;
		this.Display.CreationDate = true;
		this.Display.LastUpdateDate = true;
	
    }

    public beforeRefreshGrid() {
    }
}


myApp.controller("AccidentTypesIndexController", AccidentTypesIndexController);

