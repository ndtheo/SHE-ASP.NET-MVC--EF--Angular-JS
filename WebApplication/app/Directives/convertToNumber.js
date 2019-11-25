// Used to convert the select elements value from string to number etc.
// The select's value is always a string, so we need to change it
myApp.directive("convertToNumber", function () { return ({
    require: "ngModel",
    link: function (scope, element, attrs, ngModel) {
        ngModel.$parsers.push(function (val) {
            //saves integer to model null as null
            var retval = val === null ? null : parseInt(val, 10);
            return isNaN(retval) ? null : retval;
        });
        ngModel.$formatters.push(function (val) { return (val === null ? null : "" + val); });
    }
}); });
//# sourceMappingURL=convertToNumber.js.map