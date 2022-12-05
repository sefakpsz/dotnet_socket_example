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
    connection.send("MessageReceived", input.value).then(() => { input.value = "";});
});

connection.on("MessageSent", (input, counter) => {
    var li = document.createElement("li");
    li.innerText = input;
    messageList.appendChild(li);
    notificationCounter.innerText = counter;
});

function fulfilled() {
    notificationCounter.value = 0;
}

function reject() {
}

connection.start().then(fulfilled, reject);