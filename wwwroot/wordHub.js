"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/wordHub").build();

//Disable send button until connection is established
// document.getElementById("sendButton").disabled = true;
// connection.on("ReceiveDeleteMessage", function (id) {
//     const obj = document.getElementById(id);
//     obj.classList.remove('in');
//         obj.classList.add('out');
//         obj.style.opacity = 0;
//         obj.addEventListener("transitionend", () => {
//             obj.remove();
//         });
// });
connection.on("ReceiveMessage", function (message) {
    // var li = document.createElement("li");
    // document.getElementById("messagesList").appendChild(li);
    // // We can assign user-supplied strings to an element's textContent because it
    // // is not interpreted as markup. If you're assigning in any other way, you 
    // // should be aware of possible script injection concerns.
    // li.textContent = `${user} says ${message}`;
    // console.log(message);
    const obj = document.getElementById(message);
    obj.classList.remove('in');
        obj.classList.add('out');
        obj.style.opacity = 0;
        obj.addEventListener("transitionend", () => {
            obj.remove();
        });
});

connection.start().then(function () {
//     document.getElementById("sendButton").disabled = false;
// }).catch(function (err) {
//     return console.error(err.toString());
    console.log('connection started');
});

// document.getElementById("sendButton").addEventListener("click", function (event) {
//     var user = document.getElementById("userInput").value;
//     var message = document.getElementById("messageInput").value;
//     connection.invoke("SendMessage", user, message).catch(function (err) {
//         return console.error(err.toString());
//     });
//     event.preventDefault();
// });
document.getElementById('container').onclick = function(event) {
    const obj = event.target.closest('div')
    if (obj.classList.contains('word')){
        connection.invoke("SendMessage", obj.id).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    //     fetch('api/word/' + obj.id, { method: 'DELETE', })
    //         .then(res => res.status)
    //         .then(() => {
    //             // clearInterval(intervalId);
    //             obj.classList.remove('in');
    //             obj.classList.add('out');
    //             obj.style.opacity = 0;
    //             obj.addEventListener("transitionend", () => {
    //                 obj.remove();
    //                 // intervalId = setInterval( () => {
    //                 //     getData('api/word/after/' + maxId);
    //                 // }, 3000 );
    //             })
    //         }).catch(error => {
    //             console.error('There has been a problem with your fetch operation:', error);
    //         });
    }
};
