
class SumFilter {

    public static factory() {
        return SumFilter.filter;
    }

    public static filter(data) {
        if (angular.isUndefined(data) || data == null)
            return 0;
        var sum = 0.0;

        angular.forEach(data,
            (v: string, k) => {
                if (!angular.isUndefined(v) && v != null)
                    sum = sum + parseFloat(v);
            });
        return sum;
    }
}

myApp.filter("sum", SumFilter.factory);