"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.start();

connection.on("ReceiveMsg", function (msg) {

    var li = document.createElement("li");
    li.textContent = msg;
   /* document.getElementById("msglist").appendChild(li);*/
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        };
        toastr.info(msg);

    }, 1300);
})

