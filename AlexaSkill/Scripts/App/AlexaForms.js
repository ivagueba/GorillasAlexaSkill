$('#2').hide();

let updateFormsVisibility = function (formToDisplay) {
    $('form').trigger("reset").hide('slow');
    $('#' + formToDisplay).show('slow');
}

let updateFirstNameInput = function (newFirstName) {
    console.log(newFirstName);
    $('.exampleFirstName').text('');
    $('.firstName').val(newFirstName);
}

let updateCountryInput = function (newCountry) {
    $('.yourCountry').text('');
    $('.country').val(newCountry);
}

let updateServiceInput = function (newService) {
    $('#services').val('Service' + newService);
}

let updateAgeInputField = function (newAge) {
    $('.exampleInputAge').text('');
    $('#exampleInputAge').val(newAge);
}

let updateBudgetInput = function (newBudget) {
    $('#budget').val(newBudget);
}

let updateDateInputField = function (newDate) {
    $('#exampleDateInput').val(newDate);
}

//Start SignalR Connection with Server
$.connection.hub.start()
    .done(function () {
        console.log('SignalR connection stablished');
        //$.connection.alexaHub.server.updateFormVisibility("DING DING DING");
    })
    .fail(function () {
        console.log('SignalR connection failed')
    });

$.connection.alexaHub.client.updateFormVisibility = function (formId) {
    console.log(formId);
    updateFormsVisibility(formId);
}

$.connection.alexaHub.client.updateFirstNameInputField = function (firstName) {
    updateFirstNameInput(firstName);
}

$.connection.alexaHub.client.updateCountryInputField = function (country) {
    console.log(country);
    updateCountryInput(country);
}

$.connection.alexaHub.client.updateServicesInputField = function (service) {
    console.log(service);
    updateServiceInput(service);
}

$.connection.alexaHub.client.updateBudgetInputField = function (budget) {
    console.log(budget);
    updateBudgetInput(budget);
}

$.connection.alexaHub.client.updateAgeInputField = function (age) {
    console.log(age);
    updateAgeInputField(age);
}

$.connection.alexaHub.client.updateDateInputField = function (date) {
    console.log(date);
    updateDateInputField(date);
}
