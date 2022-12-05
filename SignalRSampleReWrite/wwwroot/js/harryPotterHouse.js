var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/harryPotterHouse")
    .build();

let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");


btn_gryffindor.addEventListener("click", function (event) {
    connection.send("Subscribe", "Gryffindor", false);
    btn_gryffindor.style.display = "none";
    btn_un_gryffindor.style.display = "";
    event.preventDefault();
})

btn_slytherin.addEventListener("click", function (event) {
    connection.send("Subscribe", "Slytherin", false);
    btn_slytherin.style.display = "none";
    btn_un_slytherin.style.display = "";
    event.preventDefault();
})

btn_hufflepuff.addEventListener("click", function (event) {
    connection.send("Subscribe", "Hufflepuff", false);
    btn_hufflepuff.style.display = "none";
    btn_un_hufflepuff.style.display = "";
    event.preventDefault();
})

btn_ravenclaw.addEventListener("click", function (event) {
    connection.send("Subscribe", "Ravenclaw", false);
    btn_ravenclaw.style.display = "none";
    btn_un_ravenclaw.style.display = "";
    event.preventDefault();
})



btn_un_gryffindor.addEventListener("click", function (event) {
    connection.send("Subscribe", "Gryffindor", true);
    btn_gryffindor.style.display = "";
    btn_un_gryffindor.style.display = "none";
    event.preventDefault();
})

btn_un_slytherin.addEventListener("click", function (event) {
    connection.send("Subscribe", "Slytherin", true);
    btn_slytherin.style.display = "";
    btn_un_slytherin.style.display = "none";
    event.preventDefault();
})

btn_un_hufflepuff.addEventListener("click", function (event) {
    connection.send("Subscribe", "Hufflepuff", true);
    btn_hufflepuff.style.display = "";
    btn_un_hufflepuff.style.display = "none";
    event.preventDefault();
})

btn_un_ravenclaw.addEventListener("click", function (event) {
    connection.send("Subscribe", "Ravenclaw", true);
    btn_ravenclaw.style.display = "";
    btn_un_ravenclaw.style.display = "none";
    event.preventDefault();
})


trigger_gryffindor.addEventListener("click", function (event) {
    connection.send("Triggering", "Gryffindor");
})

trigger_slytherin.addEventListener("click", function (event) {
    connection.send("Triggering", "Slytherin");
})

trigger_hufflepuff.addEventListener("click", function (event) {
    connection.send("Triggering", "Hufflepuff");
})

trigger_ravenclaw.addEventListener("click", function (event) {
    connection.send("Triggering", "Ravenclaw");
})

connection.on("Subbed", (house, unsubscribing, only, houseList) => {
    if (unsubscribing) {
        if (only == 'only') {
            lbl_houseJoined.innerText = houseList;
            toastr.success(`You have Unsubscribed Successfully. ${house}`);
        }
        else {
            toastr.warning(`Member has unsubscribed from ${house}`);
        }
    }
    else {
        if (only == 'only') {
            lbl_houseJoined.innerText = houseList;
            toastr.success(`You have Subscribed Successfully. ${house}`);
        }
        else {
            toastr.success(`Member has subscribed to ${house}`);
        }
    }
});

connection.on("Trigger", (house) => {
    toastr.success(`A new notification for ${house} has been launched.`);
})

function fulfilled() {

}

function reject() {

}

connection.start().then(fulfilled, reject);