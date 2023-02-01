






var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();



connection.start();
connection.on("ReceiveMessage", message => {
    $("#notify").html(message);
    $("#notify").fadeIn(2000, () => { });
});