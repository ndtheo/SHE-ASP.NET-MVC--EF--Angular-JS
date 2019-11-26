class RootController {

    public static $inject = ["$rootScope", "loader", "sheAlert"];
    private readonly  defaultTitle = "SHE CRUD system";

    constructor(private readonly $rootScope: ng.IRootScopeService, private readonly loader: Loader, private readonly sheAlert: SheAlert) {

        $rootScope.title = this.defaultTitle;
        $rootScope.onAjax = false;

        $rootScope.ShowBackground = () => $rootScope.title === this.defaultTitle;

        $rootScope.$on("$routeChangeStart",this.onRouteChangeStart);
        $rootScope.$on("$routeChangeSuccess", this.onRouteChangeSuccess);
        $rootScope.$on("$routeChangeError", this.onRouteChangeError);
        $rootScope.$on("ajax-start",this.onAjaxStart);
        $rootScope.$on("ajax-stop",this.onAjaxStop);
    }

    private onRouteChangeStart = (event, current, previous) => { }

    private onRouteChangeSuccess = (event, current, previous) => {
        if (typeof (current) == "undefined" || current == null)
            this.$rootScope.title = this.defaultTitle;
        else
            this.$rootScope.title = current.$$route.title;
    }

    private onRouteChangeError = (event, current, previous) => {
        this.sheAlert.error("Could not load page");
    }

    private onAjaxStart = () => {
        this.$rootScope.onAjax = true;
        this.loader.show();
    }

    private onAjaxStop = () => {
        this.$rootScope.onAjax = false;
        this.loader.hide();
    }
}

myApp.run(RootController);