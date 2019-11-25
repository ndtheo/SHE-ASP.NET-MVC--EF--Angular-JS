// A directive that allows a chechbox to have a middle stage

myApp.directive("ngIndeterminate",
    () => ({
        restrict: "A",
        link(scope, element, attributes) {
            attributes.$observe("ngIndeterminate",
                value => {
                    $(element).prop("indeterminate", value == "true");
                });
        }
    }))