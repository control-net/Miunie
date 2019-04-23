var $actionBtn = $("#ActionButton");
var $statusSpan = $("#StatusString");
var $botAvatar = $("#BotAvatar");

var connection = new signalR.HubConnectionBuilder().withUrl("/statusHub").build();

connection.start().then(function () {
    // on connection started
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("BotConnectionStateChanged", onBotConnectionStateChanged);

function onBotConnectionStateChanged(status, avatarUrl) {
    $statusSpan.text(status);
    $botAvatar.attr("src", avatarUrl);

    if (status == "CONNECTED") {
        $actionBtn.removeAttr("disabled");
        $actionBtn.removeClass("btn-success");
        $actionBtn.addClass("btn-danger");
        $actionBtn.text("Stop");
    } else if (status == "DISCONNECTED") {
        $actionBtn.removeAttr("disabled");
        $actionBtn.removeClass("btn-danger");
        $actionBtn.addClass("btn-success");
        $actionBtn.text("Start");
    } else {
        $actionBtn.attr("disabled", "disabled");
        $actionBtn.text("Connecting...");
    }
}

$actionBtn.click(() => {
    $actionBtn.attr("disabled", "disabled");

    connection.invoke("ToggleBotConnection").catch((err) => {
        return console.error(err.toString());
    });
});
