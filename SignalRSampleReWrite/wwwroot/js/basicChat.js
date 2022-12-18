var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/basicChat")
    .build();

var sendButton = document.getElementById("sendMessage");
var senderMail = document.getElementById("senderEmail");
var receiverEmail = document.getElementById("receiverEmail");
var chatMessage = document.getElementById("chatMessage");
var messagesList = document.getElementById("messagesList");

document.getElementById("sendMessage").addEventListener("click", function (event) {
    //if (receiverEmail.innerText.trim().length === 0) {}
    connection.send("MessageHandler", chatMessage.innerText, receiverEmail.innerText, senderMail.innerText);
    event.preventDefault();
})

connection.on("MessageReceived", (messages, messagesLength) => {
    for (var i = 0; i < messagesLength; i++) {
        var li = document.createElement("li");
        li.innerText = messages[i];
        messagesList.appendChild(li);
    }
})

function fulfilled() {

}

function reject() {

}

connection.start().then(fulfilled, reject);