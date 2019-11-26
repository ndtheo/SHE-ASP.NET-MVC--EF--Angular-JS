
abstract class BaseDetailsController {
	public static $inject = ["$scope", "$uibModal", "$uibModalInstance", "database", "documents", "id", "parentId"];

	public model: any;
	public Id: number | string;
	protected entity = null;
	protected formName: string;

	protected constructor(
		protected controllerName: string,
		protected $scope: ng.IScope,
		protected $uibModal: ng.ui.bootstrap.IModalService,
		protected $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
		protected database: Database,
		protected documents: Documents,
		protected id: number | string,
		protected parentId,
		runInit: boolean = true) {
		this.setDefaultValues();

		if (runInit) this.init();
	}

	private setDefaultValues() {
		this.formName = `${this.controllerName}Form`;
		this.Id = this.id;
		this.model = {};
	}

	protected init() {
		if (this.id > 0 || (this.id as string).length > 0) {
			var d =this.database.Load(this.controllerName, this.id, m => {
				this.model = m;
				this.afterLoad();
			});
		}
	}

	protected NewRelatedEntity(elementId, controller, parentVarName = null, arrayName = null, labelProperty = "Name") {
		const parentId = Toolkit.getFromNullableIndex(this.model, parentVarName);
		Toolkit.newRelatedObject(this.$scope, this.$uibModal, elementId, controller, `${this.controllerName}Form`, parentId, arrayName, labelProperty);
	}

	protected EditRelatedEntity(elementId, controller, labelProperty = "Name") {
		Toolkit.editRelatedObject(this.$scope, this.$uibModal, elementId, controller, labelProperty);
	}

	protected Save() {
		this.$scope[this.formName].$setPristine();
		this.database.Save(this.controllerName, this.model,
			model => {
				this.model = model;
				this.entity = angular.copy(model);
				this.Id = model.Id;
			},
			() => {
				this.$scope[this.formName].$setDirty();
			});
	}

	protected SaveAndCloseModal() {
		this.database.Save(this.controllerName, this.model, model => { this.$uibModalInstance.close(model); });
	}

	protected CloseModal() {
		this.entity != null ? this.$uibModalInstance.close(this.entity) : this.$uibModalInstance.dismiss("cancel");
	}

	protected CheckNameExists() {
		this.database.CheckNameExists(this.controllerName, this.model,
			isValid => {
				this.$scope[this.formName].Name.$setValidity("nameExists", isValid);
			});
	}

    protected afterLoad() {
        var dateFields = this.model;
    }
}
