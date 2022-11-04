var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyHallow", signalR)
    .build();

var cloak = document.getElementById("cloakCounter");
var stone = document.getElementById("stoneCounter");
var wand = document.getElementById("wandCounter");

//connection.on("Vote", value => {
//    connection.send("IncreaseToCounter", value);
//});

connection.on("UpdateCounters", (Cloak, Wand, Stone) => {
    connection.invoke("CloakCounter").then(value => {
        cloak.innerText = value.toString();
    });

    connection.invoke("WandCounter").then(value => {
        stone.innerText = value.toString();
    });

    connection.invoke("StoneCounter").then(value => {
        wand.innerText = value.toString();
    });
});


function fulfilled() {
    connection.invoke("CloakCounter").then(value => {
        cloak.innerText = value.toString();
    });

    connection.invoke("WandCounter").then(value => {
        stone.innerText = value.toString();
    });

    connection.invoke("StoneCounter").then(value => {
        wand.innerText = value.toString();
    });
}

function reject() {

}

connection.start().then(fulfilled, reject);