interface IBaseIndexScope extends ng.IScope {

    SelectedId: any;
    SearchCriteria: any;
    Pages: Array<number>;

    Select(id: number): void;
    ChangeOrderBy(order: string): void;
    NewEntity(): void;
    EditEntity(id: number | string): void;
    DeleteEntity(id: number | string): void;
    OpenSearchCriteria(): void;
    ExportGridToExcel(): void;
    RefreshGrid(): void;
}