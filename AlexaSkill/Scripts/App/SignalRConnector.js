//Start SignalR Connection with Server
$.connection.hub.start()
    .done(function () {
        console.log('SignalR connection stablished');
    })
    .fail(function () {
        console.log('SignalR connection failed');
    });


$.connection.alexaHub.client.updateFormVisibility = function (formId) {
    console.log(formId);
    const serviceFormUrl = window.location.host + (formId == 1 ? '/Home/Form' : '/Home/ServiceForm');

    window.location.assign(`http://${serviceFormUrl}`);
}