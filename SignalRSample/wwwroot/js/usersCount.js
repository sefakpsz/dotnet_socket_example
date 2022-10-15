//create connection
//configure to TransportType with help of adding comma next to url and => ",signalR.HttpTransportType.LongPolling/ServerSentEvents/WebSockets(Default)"
var connectionUserCount = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Information)       if we want to logging we can use this line of code
    .withAutomaticReconnect()
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets).build();

//connect to methods that hub invokes aka receive notifications from hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
})

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
})

//invoke hub methods aka send notification to hub
function newWindowLoadedOnClient() {
    //if we use send we don't expect any response but when we use invoke we do
    //connectionUserCount.Send("NewWindowLoaded");
    connectionUserCount.invoke("NewWindowLoaded", "Sefa").then((value) => console.log(value));
}

//start connection
function fulfilled() {
    //do something on start
    console.log("Connection to Use Hub Successful");
    newWindowLoadedOnClient();
}

function rejected() {
    //rejected logs
}

connectionUserCount.onclose((error) => {
    toastr.error("Reconnection is failed..");
});

connectionUserCount.onreconnected((connectionId) => {
    toastr.success("Reconnected");
});

connectionUserCount.onreconnecting((error) => {
    toastr.warning("Reconnecting...");
});

connectionUserCount.start().then(fulfilled, rejected);