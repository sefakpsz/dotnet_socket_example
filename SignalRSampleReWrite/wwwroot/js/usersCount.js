var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/usersCount", signalR)
    .build();

connection.on("ReceiveNotification", value => {
    var totalViewsCounter = document.getElementById("totalViewsCounter");
    totalViewsCounter.innerText = value.toString();
});

connection.on("UserConnection", value => {
    var totalUsersCounter = document.getElementById("totalUsersCounter");
    totalUsersCounter.innerText = value.toString();
});

console.log("asdfgsdaf");

function uptadeToViews() {
    connection.send("NewWindowLoaded");
}

function fulfilled() {
    uptadeToViews();
}

connection.start().then(fulfilled);