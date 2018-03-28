
let updateFormsVisibility = (formToDisplay) => {
    if (formToDisplay === "2") {
        var protocol = "http://"
        var serviceFormURL = window.location.host + '/Home/ServiceForm';
        window.location.assign(protocol + serviceFormURL);
    } else {
        console.log('Already in Contact Form Screen');
    }
}

let updateFirstNameInput = (newFirstName) => {
    console.log(newFirstName);
    $('.exampleFirstName').text('');
    $('.firstName').val(newFirstName);
}

let updateAgeInputField = (newAge) => {
    $('.exampleInputAge').text('');
    $('#exampleInputAge').val(newAge);
}

let updateDateInputField = (newDate) => {
    $('#exampleDateInput').val(newDate);
}

let updateTrainingDayInputField = (day) => {
    const days = day.split(" ");
    days.forEach(function (i) {
        $("#" + i.toLowerCase()).prop("checked", true);
    });
}

let updateGenderInputField = (gender) => {
    $('#' + gender.toLowerCase()).prop("checked", true);
}

/*$.connection.alexaHub.client.updateFormVisibility = function (formId) {
    console.log(formId);
    updateFormsVisibility(formId);
}*/

$.connection.alexaHub.client.submitForm = () => {
    console.log("I should submit now!... BUT WAIT I NEED TO VALIDATE STUFF I THINK");
    let info = validateFormInput();
    submitContactForm(info);
}

$.connection.alexaHub.client.updateFirstNameInputField = (firstName) => {
    updateFirstNameInput(firstName);
}

$.connection.alexaHub.client.updateAgeInputField = (age) => {
    console.log(age);
    updateAgeInputField(age);
}

$.connection.alexaHub.client.updateDateInputField = (date) => {
    console.log(date);
    updateDateInputField(date);
}

$.connection.alexaHub.client.updateTrainingDayInputField = (day) => {
    console.log(day);
    updateTrainingDayInputField(day);
}

$.connection.alexaHub.client.updateGenderInputField = (gender) => {
    console.log(gender);
    updateGenderInputField(gender);
}