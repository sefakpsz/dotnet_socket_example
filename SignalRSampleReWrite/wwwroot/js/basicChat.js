var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/basicChat")
    .build();

var sendButton = document.getElementById("sendMessage");
var senderMail = document.getElementById("senderEmail");
var receiverEmail = document.getElementById("receiverEmail");
var chatMessage = document.getElementById("chatMessage");
var messagesList = document.getElementById("messagesList");

document.getElementById("sendMessage").addEventListener("click", function (event) {
    connection.send("MessageHandler", chatMessage.value, receiverEmail.value, senderMail.value);
    event.preventDefault();
})

connection.on("MessageReceived", (messages) => {
    var li = document.createElement("li");
    li.innerText = messages;
    messagesList.appendChild(li);
})

function fulfilled() {
    connection.invoke("GetAllMessages").then((value) => {
        for (var i = 0; i < value.length; i++) {
            var li = document.createElement("li");
            li.innerText = value[i].toString();
            messagesList.appendChild(li);
        }
    });
}

function reject() {

}

connection.start().then(fulfilled, reject); 