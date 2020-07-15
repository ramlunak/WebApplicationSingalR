//const connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();


connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = `${user} says ${message}`;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().catch(err => console.error(err));


document.getElementById("sendButon").addEventListener("click", event => {

    connection.invoke("SendMessage", "user", "msg").catch(err => console.error(err));
    event.preventDefault();
});