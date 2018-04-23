﻿
let updateFormsVisibility = (formToDisplay) => {
    if (formToDisplay === '2') {
        var protocol = 'https://'
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
    info.firstName = $('#exampleFirstName').val();
    if (!info.firstName) {
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
    //TODO: Add GENDER AND DAYOF THE WEEK INFO AND VALIDATION
    //info.gender = $('input[name=optionsRadios]:checked').val();
    return info;
}

let submitContactForm = (contactFormData) => {
    console.log(contactFormData);
    let contactFormEndpoint = 'http://' + window.location.host + '/api/contact/submit';

    if (!contactFormData) {
        alert('GO AWAY!!')
    } else {
        $.ajax({
            type: 'POST',
            url: contactFormEndpoint,
            data: contactFormData,
            success: function (data, textStatus) { console.log(textStatus); },
            error: function (xhr, textStatus, errorThrown) { alert('failed'); }
        });
    }
}

$.connection.alexaHub.client.submitForm = () => {
    console.log('I should submit now!... BUT WAIT I NEED TO VALIDATE STUFF I THINK');
    let info = validateData();
    submitContactForm(info);
}

$.connection.alexaHub.client.updateFormField = (fieldName, value) => {
    switch (fieldName) {
        case 'firstName': 
            updateFirstNameInput(value);
            break;
        case 'date':
            updateDateInputField(value);
            break;
        case 'age':
            updateAgeInputField(value);
            break;
        case 'trainingDay':
            updateTrainingDayInputField(value);
            break;
        case 'gender':
            updateGenderInputField(value);
            break;
    }
}

$.connection.alexaHub.client.GetEmployeesList = (employeesList) => {
    console.log(employeesList);
    var htmlResult = "";
    htmlResult += "<ul><li><a href='#'>Employees by Project</a><ul>";
    $(employeesList).each(function (index, project) {       
        htmlResult += "<li><a href = '#'>" + project.Name + "</a>";
        if ($(project.Employees).length > 0) {
            htmlResult += "<ul>";
            $(project.Employees).each(function (index, employee) {
                htmlResult += "<li><a href='#'>" + employee.FirstName + "</a></li>";
            });
            htmlResult += "</ul>";
        }
        htmlResult += "</li>"; 
    });
    htmlResult += "</ul></li></ul>";
    $('#tvProjEmp').html(htmlResult);
}
