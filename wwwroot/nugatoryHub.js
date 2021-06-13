"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/nugatoryHub").build();

// initialize signalR connection
connection.start().then(function () {
    console.log('connection started');
});

// upon receipt of add message, add the word to the DOM
connection.on("ReceiveAddMessage", function (jsonString) {
    placeWords([jsonString]);
});

// upon receipt of delete message, remove the word from the DOM
connection.on("ReceiveDeleteMessage", function (id) {
    const obj = document.getElementById(id);
    obj.classList.remove('in');
        obj.classList.add('out');
        obj.style.opacity = 0;
        obj.addEventListener("transitionend", () => {
            obj.remove();
        });
});
