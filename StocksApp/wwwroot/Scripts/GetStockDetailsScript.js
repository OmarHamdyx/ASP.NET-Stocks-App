document.addEventListener('DOMContentLoaded', function () {
    const token = document.querySelector("#FinnhubToken").value;
    const stockSymbol = document.getElementById("StockSymbol").value;
    var socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

    socket.addEventListener('open', function (event) {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }));
    });

    socket.addEventListener('message', function (event) {
        var eventData = JSON.parse(event.data);
        if (eventData && eventData.data) {
            var updatedPrice = JSON.parse(eventData.data).data[0].p.toFixed(2);
            $(".stock-price").text(updatedPrice);
        }
    });

    var unsubscribe = function (symbol) {
        socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': symbol }));
        socket.close();
    };

    window.onunload = function () {
        unsubscribe(stockSymbol);
    };
});
