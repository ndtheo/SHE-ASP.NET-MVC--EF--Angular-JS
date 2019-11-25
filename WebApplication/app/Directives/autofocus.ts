myApp.directive("autofocus",
    () => ({
        restrict: "A",
        link(scope, element) {
            setTimeout(() => { element[0].focus(); }, 500);
        }
    }));