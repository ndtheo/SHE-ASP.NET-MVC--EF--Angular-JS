
class UsersIndexController extends BaseIndexController {

    public static $inject = BaseIndexController.$inject.concat(["$http", "visionAlert"]);

    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database, private readonly $http: ng.IHttpService, private readonly sheAlert: SheAlert) {
        super("Users", $scope, $uibModal, database);
        this.setupController();
        this.RefreshGrid();
    }

    public setupController() {
        this.SearchCriteria.OrderBy = "Name";

        this.Display.Name = true;
        this.Display.Email = true;
        this.Display.PhoneNumber = true;
    }

    public beforeRefreshGrid(): void {
        this.SearchCriteria.RoleId = Toolkit.getParentId(this.$scope, this.SearchCriteria.RoleId);
    }
}


myApp.controller("UsersIndexController", UsersIndexController);