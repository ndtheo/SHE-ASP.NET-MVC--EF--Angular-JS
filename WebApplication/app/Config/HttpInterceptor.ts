
class HttpInterceptor implements ng.IHttpInterceptor {

    public static $inject = ["$q", "$rootScope", "$timeout"];

    private requests: number = 0;

    public static factory($q: ng.IQService, $rootScope: ng.IRootScopeService, $timeout: ng.ITimeoutService) {
        return new HttpInterceptor($q, $rootScope, $timeout);
    }

    constructor(private readonly $q: ng.IQService, private readonly $rootScope: ng.IRootScopeService, private readonly $timeout: ng.ITimeoutService) { }

    private ajaxStopinternal = () => {
        if (!this.requests)
            this.$rootScope.$broadcast("ajax-stop");
    };

    private ajaxStart = () => {
        if (!this.requests) {
            this.$rootScope.$broadcast("ajax-start");
        }
        this.requests++;
    }

    private ajaxStop = () => {
        this.requests--;
        this.$timeout(this.ajaxStopinternal, 500);
    }

    public request = config => {
        this.ajaxStart();
        return this.$q.when(config);
    }

    public response = response => {
        this.ajaxStop();
        return this.$q.when(response);
    }

    public responseError = rejection => {
        this.ajaxStop();
        return this.$q.reject(rejection);
    };
}

myApp.config(["$httpProvider", ($httpProvider: ng.IHttpProvider) => { $httpProvider.interceptors.push(["$q", "$rootScope", "$timeout",HttpInterceptor.factory]) }]);

