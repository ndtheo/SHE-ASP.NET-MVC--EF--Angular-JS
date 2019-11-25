interface IBaseDetailsScope extends ng.IScope {
    Id: number|string;
    model;
    controllerName: string;

    NewRelatedEntity(elementId, controller, parentVarName?, arrayName?): void;
    EditRelatedEntity(elementId, controller): void;
    Save():void;
    CloseModal():void;
    SaveAndCloseModal(): void;
    CheckNameExists() : void;

}