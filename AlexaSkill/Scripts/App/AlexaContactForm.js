
let updateFormsVisibility = (formToDisplay) => {
    if (formToDisplay === '2') {
        var protocol = 'http://'
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
    const days = day.split(' ');
    days.forEach(function (i) {
        $('#' + i.toLowerCase()).prop('checked', true);
    });
}

let updateGenderInputField = (gender) => {
    $('#' + gender.toLowerCase()).prop('checked', true);
}

let validateData = () => {
    var info = {};
    info.name = $('#exampleFirstName').val();
    if (!info.name) {
        console.log('NO VALUE HERE BRO');
        $('#exampleFirstName').addClass('is-invalid');
    }

    info.age = $('#exampleInputAge').val();
    if (!info.age) {
        console.log('NO VALUE HERE BRO');
        $('#exampleInputAge').addClass('is-invalid');
    }

    info.date = $('#exampleDateInput').val();
    if (!info.date) {
        console.log('NO VALUE HERE BRO');
        $('#exampleDateInput').addClass('is-invalid');
    }
    //info.gender = $('input[name=optionsRadios]:checked').val();
    return info;
}

let submitContactForm = (info) => {
    if (!info) {
        alert('GO AWAY!!')
    }
    console.log(info);
}

$.connection.alexaHub.client.submitForm = () => {
    console.log('I should submit now!... BUT WAIT I NEED TO VALIDATE STUFF I THINK');
    let info = validateData();
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