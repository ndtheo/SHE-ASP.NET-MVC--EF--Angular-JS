
class VisionDateFilter {

    public static factory() {
        return VisionDateFilter.filter;
    }

    public static filter(item: string) {
        return item != null
            ? moment(item).format("DD/MM/YYYY")
            : "";
    }
}

myApp.filter("visionDate", VisionDateFilter.factory);


class DayMonthFilter {

    public static factory() {
        return DayMonthFilter.filter;
    }

    public static filter(item: string) {
        return item != null
            ? moment(item).format("DD/MM")
            : "";
    }
}

myApp.filter("dayMonth", DayMonthFilter.factory);
