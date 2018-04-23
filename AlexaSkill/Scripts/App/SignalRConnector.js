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
    const serviceFormUrl = window.location.host + '/Home/Form' + '?form=' + formId;

    window.location.assign(`http://${serviceFormUrl}`);
    //console.log(formId);
    //setTimeout(function () {
    //    switch (formId) {
    //        case "projects":
    //            $('form').hide();
    //            $('#formProject').show();
    //            break;
    //        case "employees":
    //            $('form').hide();
    //            $('#formEmployee').show();
    //            break;
    //        case "skills":
    //            $('form').hide();
    //            $('#formSkill').show();
    //            break;
    //    }
    //    console.log(formId);
    //}, 5000);
    //console.log(formId);
}