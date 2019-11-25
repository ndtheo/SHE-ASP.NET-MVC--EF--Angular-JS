
class RouteConfig {

	public static $inject = ["$routeProvider", "$locationProvider"];

	constructor(private readonly $routeProvider: ICustomRouteProvider, $locationProvider: ng.ILocationProvider) {
		$locationProvider.html5Mode(false);
		$locationProvider.hashPrefix("");
		this.setupRoutes();
	}

	public setupRoutes = () => {
		this.$routeProvider
			.when("/Incidents", { title: "Incidents", templateUrl: "/Incidents" })
			.when("/IncidentTypes", { title: "Incident Types", templateUrl: "/IncidentTypes" })
	}
}

myApp.config(RouteConfig);

interface ICustomRouteProvider extends ng.route.IRouteProvider {
	when(path: string, route: ICustomRoute): ICustomRouteProvider;
}

interface ICustomRoute extends ng.route.IRoute {
	title: string;
}