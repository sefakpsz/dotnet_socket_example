var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");

//create connection
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyhallows").build();

//connect to methods that hub invokes aka receive notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowCount", (cloak, stone, wand) => {
    cloakSpan.textContent = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
})


//invoke hub methods aka send notification to hub

//start connection
function fulfilled() {
    //do something on start
    console.log("Connection to Use Hub Successful");
    connectionDeathlyHallows.invoke("GetRaceStatus").then(value => {
        cloakSpan.innerText = value.cloak.toString();
        stoneSpan.innerText = value.stone.toString();
        wandSpan.innerText = value.wand.toString();
    });
}

function rejected() {
    //rejected logs
}

connectionDeathlyHallows.start().then(fulfilled, rejected);