class Toolkit {

    public static trySetPageSize(pageSize) {
    }

    public static tryGetPageSize(defaultPageSize) {
        return "All";
    }

    // ========== New Related Object ========================================================================================
    public static newRelatedObject($scope, $uibModal: ng.ui.bootstrap.IModalService, elementId, controller, formController, parentId = null, arrayName = null, labelProperty: string) {

        const modalInstance = $uibModal.open({
            animation: true,
            templateUrl: `/${controller}/Details?noCacheHack=${Date.now()}`,
            controller: `${controller}DetailsController`,
            controllerAs: "ctrl",
            backdrop: "static",
            size: "xl",
            resolve: {
                id: 0,
                parentId() {
                    return parentId;
                }
            }
        });

        modalInstance.result.then(newOption => {
            Toolkit.selectAddSelectedOption(newOption, elementId, $scope, formController, arrayName, labelProperty);
        });
    }

    // ========== Select Add Selected Option =================================================================================
    public static selectAddSelectedOption(option, selectId, scope, formController, arrayName = null, labelProperty) {
        if (arrayName == null) {
            Toolkit.selectAddOptionInDom(option, selectId, labelProperty);
        } else {
            const index = arrayName;
            scope.ctrl[index].push(option);
        }
        scope[formController][selectId].$setViewValue(option.Id);
    }


    // ========== Select Add Option =========================================================================================
    public static selectAddOptionInDom(option, selectId: string, labelProperty): void {
        const select = document.getElementById(selectId) as HTMLSelectElement;
        const opt = document.createElement("option");
        opt.value = option.Id.toString();
        opt.label = option[labelProperty];
        opt.innerHTML = option.Name;
        opt.selected = true;
        select.appendChild(opt);
        Toolkit.sortSelectOptions(select);
    }

    // ========== Edit Related Object =======================================================================================
    public static editRelatedObject($scope, $uibModal: ng.ui.bootstrap.IModalService, elementId: string, controller: string, labelProperty: string) {
        const modalInstance = $uibModal.open({
            animation: true,
            templateUrl: `/${controller}/Details/?editMode=true&noCachehack=${Date.now()}`,
            controller: controller + "DetailsController",
            controllerAs: "ctrl",
            backdrop: "static",
            size: "xl",
            resolve: {
                id() {
                    return $scope.ctrl.model[elementId];
                },
                parentId() {
                    return 0;
                }
            }
        });
        modalInstance.result.then(option => { Toolkit.selectUpdateSelectedOption(option, elementId, labelProperty); });
    }

    // ========== Select Update Selected Option =============================================================================
    public static selectUpdateSelectedOption(option, selectId: string, labelProperty): void {
        const select = document.getElementById(selectId) as HTMLSelectElement;
        select.options[select.selectedIndex].label = option[labelProperty];   
    }

    // ========== Empty Selected Options ====================================================================================
    public static emptySelectOptions(elementId: string): void {
        const select = document.getElementById(elementId) as HTMLSelectElement;
        const length = select.options.length;
        for (let i = length - 1; i >= 0; i--) {
            select.options[i] = null;
        }
    }

    // ========== Get Order By ==============================================================================================
    public static getOrderBy(scopeOrderBy: string, order: string): string {
        if (scopeOrderBy === order) {
            scopeOrderBy += " desc";
        } else {
            scopeOrderBy = order;
        }
        return scopeOrderBy;
    }

    // ========== Open Search Criteria ======================================================================================
    public static openSearchCriteria(controller: string, searchCriteria, $uibModal: ng.ui.bootstrap.IModalService): ng.ui.bootstrap.IModalServiceInstance {
        return $uibModal.open({
            animation: true,
            templateUrl: `../${controller}/SearchCriteria/?noCachehack=${Date.now()}`,
            controller: "SearchCriteriaController",
            controllerAs: "ctrl",
            backdrop: "static",
            size: "xl",
            resolve: {
                criteria() {
                    return searchCriteria;
                }
            }
        });
    }

    // ========== Open Details Panel ========================================================================================
    public static openDetailsPanel($uibModal: ng.ui.bootstrap.IModalService, controllerName: string, id: number | string, parentId:number | string = null): ng.ui.bootstrap.IModalServiceInstance {
        /// <summary>Opens a details window</summary>
		const isEdit = id != null && id > 0;
        let url = `../${controllerName}/Details/?editMode=${isEdit}&noCachehack=${Date.now()}`;
        let controllerFinalName = controllerName + "DetailsController";
        return $uibModal.open({
            animation: true,
            templateUrl: url,
            controller: controllerFinalName,
            controllerAs: "ctrl",
            backdrop: "static",
            size: "xl",
            resolve: {
                id() {
                    return id;
                },
                parentId() {
                    return parentId;
                }
            }
        });
    }

    // ========== Open Add Existing Panel ===================================================================================
    public static openAddExistingPanel($uibModal: ng.ui.bootstrap.IModalService, controller: string, id: number | string): ng.ui.bootstrap.IModalServiceInstance {
        return $uibModal.open({
            animation: true,
            backdrop: "static",
            templateUrl: `../${controller}/AddExisting/?id=${id}&noCachehack=${Date.now()}`,
            size: "md"
        });
    }

    // ========== Get Pages =================================================================================================
    public static getPages(data): number[] {
        var pages = [];
        for (var i = 1; i <= (data.TotalRecords / data.PageSize) + 1; i++) {
            pages.push(i);
        }
        return pages;
    }

    /**
    * Reads the parent scope if exists, and returns it's id which is usually the parent id of these items when the index is used as a tab.
    */
    public static getParentId($scope, defaultValue = null) {
		try {
			//if ($scope.$parent.ctrl.id) {
				defaultValue = $scope.$parent.ctrl.Id;
			//}
        } finally {
            return defaultValue;
        }
    }

    /**
    * Reads the parent scope if exists, and returns it's name which is usually the parent id of these items when the index is used as a tab.
    */
    public static getParentName($scope): string {
        let name = null;
        try {
            name = $scope.$parent.ctrl.model.Name;
        } finally {
            return name;
        }
    }

    // ========== Sort Selected Options ====================================================================================
    public static sortSelectOptions(selElem: HTMLSelectElement) {
        /// <summary>Sorts the options of a select list</summary>
        var tmpAry = new Array();
        for (var i = 0; i < selElem.options.length; i++) {
            tmpAry[i] = new Array();
            tmpAry[i][0] = (selElem.options[i] as HTMLOptionElement).text;
            tmpAry[i][1] = (selElem.options[i] as HTMLOptionElement).value;
            tmpAry[i][2] = (selElem.options[i] as HTMLOptionElement).selected;
        }
        tmpAry.sort();
        while (selElem.options.length > 0) {
            selElem.options[0] = null;
        }
        for (var i = 0; i < tmpAry.length; i++) {
            var op = new Option(tmpAry[i][0], tmpAry[i][1], false, tmpAry[i][2]);
            selElem.options[i] = op;
        }
        return;
    }


    // ========== Has Value =================================================================================================
    public static hasValue(variable) {
        return typeof (variable) !== "undefined" && variable !== null && variable !== "";
    }

    public static removeByAttr(array, attr, value) {
        let i = array.length;
        while (i--) {
            if (array[i] && array[i].hasOwnProperty(attr) && (arguments.length > 2 && array[i][attr] === value)) {
                array.splice(i, 1);
            }
        }
        return array;
    }

    public static clearMultiselect(multiselect, selected) {
        for (let j = 0; j < selected.length; j++) {
            multiselect = Toolkit.removeByAttr(multiselect, "Id", selected[j].Id);
        }
        return multiselect;
    }

    public static getFromNullableIndex<T>(dataArray: Array<T>, index: any): T {
        if (index == null)
            return null;
        return dataArray[index];
    }

    public static findScopeByControllerName(controllerName: string): ng.IScope {
        const sel = `div[ng-controller="${controllerName}"]`;
        return angular.element(sel).scope();
    }
}