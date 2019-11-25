
class UsersIndexController extends BaseIndexController {

    constructor($scope: IBaseIndexScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("Users", $scope, $uibModal, database);
        this.setupController();
        this.$scope.RefreshGrid();
    }

    public setupController() {
        this.$scope.Display.Name = true;
        this.$scope.Display.Email = true;
        this.$scope.Display.PhoneNumber = true;
    }
}

class UsersDetailsController extends BaseDetailsController {

    public static $inject = BaseDetailsController.$inject.concat(["$http", "visionAlert"]);

    constructor($scope: IBaseDetailsScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId, private readonly $http: ng.IHttpService, private readonly visionAlert: VisionAlert) {
        super("Users", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);

        this.$scope.AddUserToRole = this.addUserToRole;
        this.$scope.RemoveUserFromRole = this.removeUserFromRole;
    }

    public removeUserFromRole = () => {
        var roleName = VisionToolkit.getParentName(this.$scope);
        VisionToolkit.removeUserFromRole(this.$http, this.$scope.SelectedId, roleName,
            () => {
                this.visionAlert.success("Role removed from user");
                this.$scope.RefreshGrid();
            });
    };

    public addUserToRole = () => {
        var modalInstance = VisionToolkit.openAddExistingPanel(this.$uibModal,this.$scope.controllerName,VisionToolkit.getParentId(this.$scope));
        var roleName = VisionToolkit.getParentName(this.$scope);

        modalInstance.result.then(userId => {
            VisionToolkit.addUserToRole(this.$http, userId, roleName,
                () => {
                    this.visionAlert.success("Role added to user");
                    this.$scope.RefreshGrid();
                });
        });
    }
}

myApp.controller("UsersIndexController", UsersIndexController);
myApp.controller("UsersDetailsController", UsersDetailsController);