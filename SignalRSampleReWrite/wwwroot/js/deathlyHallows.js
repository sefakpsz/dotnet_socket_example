var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyHallow")
    .build();


connection.on("UpdateCounters", (cloak, stone, wand) => {
    stoneSpan.textContent = stone.toString();
    cloakSpan.textContent = cloak.toString();
    wandSpan.textContent = wand.toString();
});



function fulfilled() {
    connection.invoke("CloakCounter").then(value => {
        cloak.innerText = value.toString();
    });

    connection.invoke("WandCounter").then(value => {
        wand.innerText = value.toString();
    });

    connection.invoke("StoneCounter").then(value => {
        stone.innerText = value.toString();
    });
}

function reject() {

}

connection.start().then(fulfilled, reject);