
class UserLogsIndexController extends BaseIndexController {

    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("UserLogs", $scope, $uibModal, database);
        this.setupController();
        this.RefreshGrid();
    }

    public setupController() {
		this.Display.Creator = true;
		this.Display.LastUpdateUser = true;
		this.Display.UserID = true;
		this.Display.DateLogin = true;
		this.Display.DateLogout = true;
		this.Display.Ip = true;
		this.Display.CreationDate = true;
		this.Display.LastUpdateDate = true;
	
    }

    public beforeRefreshGrid() {
    }
}


myApp.controller("UserLogsIndexController", UserLogsIndexController);

