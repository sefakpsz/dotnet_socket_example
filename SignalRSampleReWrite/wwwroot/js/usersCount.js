var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/usersCount")
    .build();

var totalViewsCounter = document.getElementById("totalViewsCounter");
var totalUsersCounter = document.getElementById("totalUsersCounter");

connection.on("ReceiveNotification", value => {

    totalViewsCounter.innerText = value.toString();
});

connection.on("UserConnected", value => {
    totalUsersCounter.innerText = value.toString();
});

connection.on("UserDisconnected", value => {
    totalUsersCounter.innerText = value.toString();
});

connection.start();