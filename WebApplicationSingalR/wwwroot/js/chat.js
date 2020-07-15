const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();

/* this is here to show an alternative to start, with a then
connection.start().then(() => console.log("connected"));
*/

/* this is here to show another alternative to start, with a catch
connection.start().catch(err => console.error(err));
*/

connection.on("ReceiveMessage", (user, message) => {
    const encodedMsg = `${user} says ${message}`;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);
    console.log(encodedMsg);
});


document.getElementById("sendButton").addEventListener("click", event => {

    connection.invoke("SendMessage", "user", "msg").catch(err => console.error(err));
    event.preventDefault();
});