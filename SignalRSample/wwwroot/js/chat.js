var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .withAutomaticReconnect([0, 1000, 5000])
    .build();

connection.on("ReceiveUserConnected", function (userId, userName, isOldConnection) {
    if (!isOldConnection) {
        addMessage(`${userName} has openned a connection`);
    }
});

function addMessage(msg) {
    if (msg == null && msg == '') {
        return;
    }
    let ui = document.getElementById('messagesList');
    let li = document.createElement("li");
    li.innerHTML = msg;
    ui.appendChild(li);
};

connection.start();