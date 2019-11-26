
class Loader {

    public static $inject = ["$loading"];

    private readonly loading: any;

	constructor(private readonly $loading) {
        this.loading = new $loading({
            busyText: "Loading process running...",
            theme: "info",
            timeout: false,
            delayHide: 300
        });
    }

    public show() {
        this.loading.show();
        console.log("loading start");
    }

    public hide() {
        this.loading.hide();
        console.log("loading stop");
    }
}


myApp.service("loader", Loader);