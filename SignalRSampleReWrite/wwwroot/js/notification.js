var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification")
    .build();

var input = document.getElementById("notificationInput");
var sendButton = document.getElementById("sendButton");
var messageList = document.getElementById("messageList");
var notificationCounter = document.getElementById("notificationCounter");
var bell = document.getElementById("notificationBell");

sendButton.addEventListener("click", function (event) {
    event.preventDefault();
    connection.send("MessageReceived", input.value).then(() => { input.value = ""; });
});

connection.on("LoadMessages", (counter, messages) => {
    messageList.innerHTML = "";
    for (var i = 0; i < messages.length; i++) {
        var li = document.createElement("li");
        li.innerText = messages[i];
        messageList.appendChild(li);
        notificationCounter.innerText = counter;
    }
});

function fulfilled() {
    connection.send("MessagesAndCounter");
}

function reject() {
}

connection.start().then(fulfilled, reject);