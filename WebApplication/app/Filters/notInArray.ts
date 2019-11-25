
class NotInArrayFilter {

    public static factory() {
        return NotInArrayFilter.filter;
    }

    public static filter(data: Array<any>, filterBy: Array<any>) {
        if (angular.isUndefined(filterBy))
            return data;

        var newData = [];

        angular.forEach(data, v => {
            if (filterBy.every(x => x.Id !== v.Id)) {
                newData.push(v);
            }
        });

        newData.sort((a, b) => ((a.Name > b.Name) ? 1 : ((a.Name < b.Name) ? -1 : 0)));
        return newData;
    }
}

myApp.filter("notInArray", NotInArrayFilter.factory);