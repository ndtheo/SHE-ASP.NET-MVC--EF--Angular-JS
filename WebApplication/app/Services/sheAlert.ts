
class SheAlert {

    public static $inject = ["$alert"];

    constructor(private readonly $alert: Function) {}

    public success(message, alertTitle = "Success") {
        this.$alert({
            title: alertTitle,
            content: message,
            alertType: "success",
            placement: "top-right",
            duration: 4000
        });
    }

    public info(message, alertTitle = "Info") {
        this.$alert({
            title: alertTitle,
            content: message,
            alertType: "info",
            placement: "top-right",
            duration: 4000
        });
    }

    public warning(message, alertTitle = "Warning") {
        this.$alert({
            title: alertTitle,
            content: message,
            alertType: "warning",
            placement: "top-right",
            duration: 4000
        });
    }

    public error(message, alertTitle = "Error") {
        const timeNeeded = this.getAlertDuration(message);
        this.$alert({
            title: alertTitle,
            content: message,
            alertType: "danger",
            placement: "top-right",
            duration: 4000 + timeNeeded
        });
    }

    private getAlertDuration(message: string) {
        const words = message.split(" ").length;
        const timeNeeded = words * 200;
        if (timeNeeded > 8000) {
            return 8000;
        }
        return timeNeeded;
    }
}

myApp.service("sheAlert", SheAlert);