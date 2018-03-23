
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

let updateAgeInputField = function (newAge) {
    $('.exampleInputAge').text('');
    $('#exampleInputAge').val(newAge);
}

let updateDateInputField = function (newDate) {
    $('#exampleDateInput').val(newDate);
}


/*$.connection.alexaHub.client.updateFormVisibility = function (formId) {
    console.log(formId);
    updateFormsVisibility(formId);
}*/

$.connection.alexaHub.client.updateFirstNameInputField = function (firstName) {
    updateFirstNameInput(firstName);
}

$.connection.alexaHub.client.updateAgeInputField = function (age) {
    console.log(age);
    updateAgeInputField(age);
}

$.connection.alexaHub.client.updateDateInputField = function (date) {
    console.log(date);
    updateDateInputField(date);
}
