
abstract class BaseIndexController implements angular.IController
{
    public static $inject = ["$scope", "$uibModal", "database"];

    public Display: any;
    public SearchCriteria: any;
    public Pages: any;
    public SelectedId: any;
    public TotalRecords: any;
	public Filter: any;
    $onInit() { }
    protected constructor(
        protected controllerName: string,
        protected $scope: ng.IScope,
        protected $uibModal: angular.ui.bootstrap.IModalService,
        protected database: Database)
    {
        this.setDefaultValues();
        this.SearchCriteria.PageSize = Toolkit.tryGetPageSize(this.SearchCriteria.PageSize);
    }

    private setDefaultValues() {
        this.Display = {};
        this.SearchCriteria = { Page: 1, PageSize: 50 };
        this.Pages = [1];
    }

    protected Select(id: number | string) {
        this.SelectedId = this.SelectedId === id ? null : id;
    }

    protected NewEntity(): void {
        const modalInstance = Toolkit.openDetailsPanel(this.$uibModal, this.controllerName, 0, Toolkit.getParentId(this.$scope));
        modalInstance.result.then(() => this.RefreshGrid(), () => this.RefreshGrid());
    }

    protected EditEntity(id: number | string) {
        const modalInstance = Toolkit.openDetailsPanel(this.$uibModal, this.controllerName, id);
        modalInstance.result.then(() => this.RefreshGrid(), () => this.RefreshGrid());
    }

    protected OpenSearchCriteria() {
        const modalInstance = Toolkit.openSearchCriteria(this.controllerName, this.SearchCriteria, this.$uibModal);
        this.Filter = null;
        modalInstance.result.then(criteria => {
            this.SearchCriteria = criteria;
            this.RefreshGrid();
        });
    }

    protected DeleteEntity() {
        this.database.Delete(this.controllerName, this.SelectedId, () => this.RefreshGrid(), () => this.RefreshGrid());
    }

    protected ExportGridToExcel() {
        this.database.ExportGridToExcel(this.controllerName, this.SearchCriteria, this.Display);
    }

    protected ChangeOrderBy = (order: string) => {
        this.SearchCriteria.OrderBy = Toolkit.getOrderBy(this.SearchCriteria.OrderBy, order);
        this.RefreshGrid();
    }

    /** This method does the following:
     *     2. Gets the page-size
     *     3. Adds the current Filters through the use of this.Filter.
     *     4. Calls the Search method of the database TS class, using controllerName, SearchCriteria as arguments
     *          This method calls the related Search Endpoints in the Controllers. 
     *          An improvement would be to place the Search actions inside the ApiControllers.        
     *  */
    protected RefreshGrid() {
        Toolkit.trySetPageSize(this.SearchCriteria.PageSize);
        this.beforeRefreshGrid();
        this.SearchCriteria.Filter = this.Filter;
        this.database.Search(this.controllerName, this.SearchCriteria, (response) => this.afterSuccessfullSearch(response));
    };

    protected afterSuccessfullSearch(response) {
        this[this.controllerName] = response.data.Data;
        this.Pages = Toolkit.getPages(response.data);
        this.TotalRecords = response.data.TotalRecords;
        this.SelectedId = null;
    };

    protected beforeRefreshGrid() {}
}
