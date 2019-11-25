
class BooleanFilter {

    public static factory() {
        return BooleanFilter.filter;
    }

    public static filter(item) {
        if (item === true) return "Yes";
        if (item === false) return "No";
        return item;
    }
}

myApp.filter("boolean", BooleanFilter.factory);