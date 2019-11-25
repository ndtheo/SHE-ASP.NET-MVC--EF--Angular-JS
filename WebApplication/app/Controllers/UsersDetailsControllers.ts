
class UsersDetailsController extends BaseDetailsController {
    $onInit() { }
    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId) {
        super("Users", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
    }
}


myApp.controller("UsersDetailsController", UsersDetailsController);