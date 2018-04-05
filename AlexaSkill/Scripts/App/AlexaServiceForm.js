
let updateFormsVisibility = (formToDisplay) => {
    if (formToDisplay === "1") {
        var protocol = "http://"
        var serviceFormURL = window.location.host + '/Home/Form';
        window.location.assign(protocol + serviceFormURL);
    } else {
        console.log('Already in Service Form Screen');
    }
}

let updateFirstNameInput = (newFirstName) => {
    console.log(newFirstName);
    $('.exampleFirstName').text('');
    $('.firstName').val(newFirstName);
}

let updateCountryInput = (newCountry) => {
    $('.yourCountry').text('');
    $('.country').val(newCountry);
}

let updateServiceInput = (newService) => {
    $('#services').val('Service' + newService);
}

let updateBudgetInput = (newBudget) => {
    $('#budget').val(newBudget);
}

/*$.connection.alexaHub.client.updateFormVisibility = function (formId) {
    console.log(formId);
    updateFormsVisibility(formId);
}*/

$.connection.alexaHub.client.updateFirstNameInputField = (firstName) => {
    updateFirstNameInput(firstName);
}

$.connection.alexaHub.client.updateCountryInputField = (country) => {
    console.log(country);
    updateCountryInput(country);
}

$.connection.alexaHub.client.updateServicesInputField = (service) => {
    console.log(service);
    updateServiceInput(service);
}

$.connection.alexaHub.client.updateBudgetInputField = (budget) => {
    console.log(budget);
    updateBudgetInput(budget);
}

