
class MenuItemsIndexController extends BaseIndexController {
    constructor($scope: IBaseIndexScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("MenuItems", $scope, $uibModal, database);
        this.setupController();
        this.$scope.RefreshGrid();
    }

    public setupController() {
        this.$scope.Display.Creator = true;
        this.$scope.Display.LastUpdateUser = true;
        this.$scope.Display.ParentMenu = true;
        this.$scope.Display.Name = true;
        this.$scope.Display.DisplayName = true;
        this.$scope.Display.ControllerName = true;
        this.$scope.Display.DefaultAction = true;
        this.$scope.Display.Disabled = true;
        this.$scope.Display.Hidden = true;
        this.$scope.Display.Icon = true;
        this.$scope.Display.OrderNumber = true;
        this.$scope.Display.EditableRights = true;
        this.$scope.Display.CreationDate = true;
        this.$scope.Display.LastUpdateDate = true;
    }
}

class MenuItemsDetailsController extends BaseDetailsController {
    constructor($scope: IBaseDetailsScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId) {
        super("MenuItems", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
    }
}


myApp.controller("MenuItemsIndexController", MenuItemsIndexController);
myApp.controller("MenuItemsDetailsController", MenuItemsDetailsController);