
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
        this.SearchCriteria.RoleId = VisionToolkit.getParentId(this.$scope, this.SearchCriteria.RoleId);
    }

    public AddUserToRole() {
        const modalInstance = VisionToolkit.openAddExistingPanel(this.$uibModal, this.controllerName, VisionToolkit.getParentId(this.$scope));
        var roleName = VisionToolkit.getParentName(this.$scope);

        modalInstance.result.then(userId => {
            VisionToolkit.addUserToRole(this.$http, userId, roleName,
                () => {
                    this.sheAlert.success("Role added to user");
                    this.RefreshGrid();
                });
        });
    }

    public RemoveUserFromRole() {
        const roleName = VisionToolkit.getParentName(this.$scope);
        VisionToolkit.removeUserFromRole(this.$http, this.SelectedId, roleName,
            () => {
                this.sheAlert.success("Role removed from user");
                this.RefreshGrid();
            });
    };

}


myApp.controller("UsersIndexController", UsersIndexController);