
class RightsIndexController extends BaseIndexController {
    constructor($scope: IBaseIndexScope, $uibModal: ng.ui.bootstrap.IModalService, database: Database) {
        super("Rights", $scope, $uibModal, database);
        this.setupController();
        this.$scope.RefreshGrid();
    }

    public setupController() {
        this.$scope.Display.Creator = true;
        this.$scope.Display.LastUpdateUser = true;
        this.$scope.Display.MenuItem = true;
        this.$scope.Display.Role = true;
        this.$scope.Display.View = true;
        this.$scope.Display.Create = true;
        this.$scope.Display.Edit = true;
        this.$scope.Display.Delete = true;
        this.$scope.Display.Export = true;
        this.$scope.Display.CreationDate = true;
        this.$scope.Display.LastUpdateDate = true;

        this.$scope.RefreshGrid = this.refreshGrid;
    }

    public refreshGrid = () => {
        this.$scope.SearchCriteria.RoleId = VisionToolkit.getParentId(this.$scope, this.$scope.SearchCriteria.RoleId);
        this.defaultRefreshGrid();
    }
}

class RightsDetailsController extends BaseDetailsController {

    constructor($scope: IBaseDetailsScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId, private readonly $http: ng.IHttpService, private readonly visionAlert: VisionAlert) {
        super("Rights", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
        $scope.model = { RoleId: parentId };
    }
}


myApp.controller("RightsIndexController", RightsIndexController);
myApp.controller("RightsDetailsController", RightsDetailsController);