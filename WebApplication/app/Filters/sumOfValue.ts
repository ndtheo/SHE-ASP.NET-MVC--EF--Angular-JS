
class SumOfValueFilter {

    public static factory() {
        return SumOfValueFilter.filter;
    }

    public static filter(data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key) || data == null || key == null)
            return 0;
        var sum = 0.0;

        angular.forEach(data,
            (v, k) => {
                if (!angular.isUndefined(v[key]) && v[key] != null)
                    sum = sum + parseFloat(v[key]);
            });
        return sum;
    }
}

myApp.filter("sumOfValue", SumOfValueFilter.factory);