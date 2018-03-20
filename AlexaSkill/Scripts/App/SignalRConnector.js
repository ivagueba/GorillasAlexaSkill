//Start SignalR Connection with Server
$.connection.hub.start()
    .done(function () {
        console.log('SignalR connection stablished');
    })
    .fail(function () {
        console.log('SignalR connection failed')
    });