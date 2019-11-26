
class IncidentTypesDetailsController extends BaseDetailsController {
    $onInit() { }
    constructor($scope: ng.IScope, $uibModal: ng.ui.bootstrap.IModalService, $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, database: Database, documents: Documents, id, parentId) {
        super("IncidentTypes", $scope, $uibModal, $uibModalInstance, database, documents, id, parentId);
		    }
}


myApp.controller("IncidentTypesDetailsController", IncidentTypesDetailsController);

