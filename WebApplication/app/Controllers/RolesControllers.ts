// ==========  Angular Index Controller  ================================
myApp.controller("RolesIndexController",
    [
        "$scope", "$http", "visionAlert", "$uibModal", "database",
        ($scope, $http, visionAlert: VisionAlert, $uibModal: ng.ui.bootstrap.IModalService, database: Database) => {

            $scope.controllerName = "Roles";

            $scope.Select = id => {
                $scope.SelectedId = $scope.SelectedId === id ? null : id;
            };

            $scope.NewEntity = () => {
                var modalInstance = VisionToolkit
                    .openDetailsPanel($uibModal, $scope.controllerName, 0, VisionToolkit.getParentId($scope));
                modalInstance.result.then($scope.RefreshGrid, $scope.RefreshGrid);
            };

            $scope.EditEntity = id => {
                var modalInstance = VisionToolkit.openDetailsPanel($uibModal, $scope.controllerName, id);
                modalInstance.result.then($scope.RefreshGrid, $scope.RefreshGrid);
            };

            $scope.OpenSearchCriteria = () => {
                var modalInstance = VisionToolkit
                    .openSearchCriteria($scope.controllerName, $scope.SearchCriteria, $uibModal);
                modalInstance.result.then(criteria => {
                    $scope.SearchCriteria = criteria;
                    $scope.RefreshGrid();
                });
            };

            $scope.DeleteEntity = () => {
                database.Delete($scope.controllerName, $scope.SelectedId, $scope.RefreshGrid, $scope.RefreshGrid);
            };

            $scope.ExportGridToExcel = () => {
                database.ExportGridToExcel($scope.controllerName, $scope.SearchCriteria, $scope.Display);
            };

            $scope.RefreshGrid = () => {
                $scope.SearchCriteria.UserId = VisionToolkit.getParentId($scope); // (For tab support)
                database.Search($scope.controllerName,
                    $scope.SearchCriteria,
                    response => {
                        $scope[$scope.controllerName] = response.data.Data;
                        $scope.Pages = VisionToolkit.getPages(response.data);
                        $scope.TotalRecords = response.data.TotalRecords;
                        $scope.SelectedId = null;
                    });
            };

            $scope.ChangeOrderBy = order => {
                $scope.SearchCriteria.OrderBy = VisionToolkit.getOrderBy($scope.SearchCriteria.OrderBy, order);
                $scope.RefreshGrid();
            };

            $scope.AddUserToRole = () => {
                var modalInstance = VisionToolkit
                    .openAddExistingPanel($uibModal, $scope.controllerName, VisionToolkit.getParentId($scope));
                var userId = VisionToolkit.getParentId($scope);

                modalInstance.result.then(roleName => {
                    VisionToolkit.addUserToRole($http,
                        userId,
                        roleName,
                        () => {
                            visionAlert.success("Role added to user");
                            $scope.RefreshGrid();
                        });
                });
            };

            $scope.RemoveUserFromRole = () => {
                var userId = VisionToolkit.getParentId($scope);
                var roleName = $scope.Roles.find(role => (role.Id === $scope.SelectedId)).Name;
                VisionToolkit.removeUserFromRole($http,
                    userId,
                    roleName,
                    () => {
                        visionAlert.success("Role removed from user");
                        $scope.RefreshGrid();
                    });
            };

            // Initialize controller
            $scope.SearchCriteria = { Page: 1, PageSize: 50 };
            $scope.Pages = [1];

            $scope.Display = {};
            $scope.Display.Name = true;

            $scope.RefreshGrid();
        }
    ]
);

// ==========  Angular Details Controller  ==============================
myApp.controller("RolesDetailsController",
    [
        "$scope", "$uibModal", "$uibModalInstance", "database", "id", "parentId", "$timeout",
        ($scope,$uibModal: ng.ui.bootstrap.IModalService,$uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,database: Database,id,parentId,$timeout: ng.ITimeoutService) => {

            var entity = null;
            $scope.controllerName = "Roles";

            $scope.NewRelatedEntity = (elementId, controller, parentVarName = null, arrayName = null) => {
                const parentId = VisionToolkit.getFromNullableIndex($scope.model, parentVarName);
                VisionToolkit.newRelatedObject($scope,$uibModal,elementId,controller,`${$scope.controllerName}Form`,parentId,arrayName);
            };

            $scope.EditRelatedEntity = (elementId, controller) => {
                VisionToolkit.editRelatedObject($scope, $uibModal, elementId, controller);
            };

            $scope.SaveAndCloseModal = () => {
                database.Save($scope.controllerName, $scope.model, model => { $uibModalInstance.close(model); });
            };

            $scope.Save = () => {
                $scope[`${$scope.controllerName}Form`].$setPristine();
                database.Save($scope.controllerName,
                    $scope.model,
                    model => {
                        $scope.model = model;
                        entity = angular.copy(model);
                        $timeout(() => { $scope.$apply(() => { $scope.Id = model.Id; }); }, 0);
                    },
                    () => { $scope[`${$scope.controllerName}Form`].$setDirty(); });
            };

            $scope.CloseModal = () => {
                entity != null ? $uibModalInstance.close(entity) : $uibModalInstance.dismiss("cancel");
            };

            $scope.CheckNameExists = () => {
                database.CheckNameExists($scope.controllerName,
                    $scope.model,
                    isValid => {
                        $scope[`${$scope.controllerName}Form`].Name.$setValidity("nameExists", isValid);
                    });
            };

            // Initialize Controller
            $scope.Id = id;
            $scope.model = {};

            if (id) {
                database.Load($scope.controllerName, id, model => { $scope.model = model; });
            }
        }
    ]
);