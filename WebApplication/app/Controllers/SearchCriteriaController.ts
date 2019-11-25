
class SearchCriteriaController {

    public static $inject = ["$scope", "$uibModalInstance", "criteria"];

    private readonly model: any;
    $onInit() { }
    constructor($scope, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, criteria) {
        this.model = criteria;
    }

    public SaveAndCloseModal() {
        this.$uibModalInstance.close(this.model);
    }

    public CloseModal() {
        this.$uibModalInstance.dismiss("cancel");
    }
}


myApp.controller("SearchCriteriaController", SearchCriteriaController);