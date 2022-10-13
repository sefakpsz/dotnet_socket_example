var connectionChat = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

document.getElementById("sendMessage").disabled = true;

connectionChat.on("MessageReceived", function (user, message) {
    var li = document.createElement("li");
    li.textContent = `${user} - ${message}`;
    document.getElementById("messageList").appendChild(li);
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var message = document.getElementById("chatMessage").value;
    var sender = document.getElementById("senderEmail").value;

    connectionChat.send("SendMessageToAll", sender, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
})

connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
});