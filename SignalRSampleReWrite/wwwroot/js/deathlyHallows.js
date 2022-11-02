var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyHallow", signalR)
    .build();

connection.on("Vote", value => {
    connection.send("IncreaseToCounter", value);
});

connection.on("UpdateCounters", (Cloak, Wand, Stone) => {
    document.getElementById("cloakCounter").innerText = Cloak.toString();
    document.getElementById("stoneCounter").innerText = Stone.toString();
    document.getElementById("wandCounter").innerText = Wand.toString();
});


function fulfilled() {

}

function reject() {

}

connection.start().then(fulfilled, reject);