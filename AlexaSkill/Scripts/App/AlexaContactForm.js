
let updateFormsVisibility = function (formToDisplay) {
    if (formToDisplay === "2") {
        var protocol = "http://"
        var serviceFormURL = window.location.host + '/Home/ServiceForm';
        window.location.assign(protocol + serviceFormURL);
    } else {
        console.log('Already in Contact Form Screen');
    }
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
