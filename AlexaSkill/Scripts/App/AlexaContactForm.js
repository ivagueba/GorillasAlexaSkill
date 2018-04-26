
let updateFormsVisibility = (formToDisplay) => {
    if (formToDisplay === '2') {
        var protocol = 'https://'
        var serviceFormURL = window.location.host + '/Home/ServiceForm';
        window.location.assign(protocol + serviceFormURL);
    } else {
        console.log('Already in Contact Form Screen');
    }
}
let updateProjectNameInput = (newProjectName) => {
    console.log(newProjectName);
    $('.projectName').val(newProjectName);
}

let updateFirstNameInput = (newFirstName) => {
    console.log(newFirstName);
    $('.exampleFirstName').text('');
    $('.firstName').val(newFirstName);
}
let updateLastNameInput = (newLastName) => {
    console.log(newLastName);
    $('.lastName').val(newLastName);
}
let updateEmailInput = (newEmail) => {
    console.log(newEmail);
    $('.email').val(newEmail);
}
let updateSlackIdInput = (newSlackId) => {
    console.log(newSlackId);
    $('.slackId').val(newSlackId);
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

let validateDataProject = () => {
    var info = {};
    info.Name = $('#Name').val();
    if (!info.Name) {
        console.log('NO VALUE HERE BRO');
        $('#Name').addClass('is-invalid');
        $('#error-messages-proj').html('All fields are required.');
    }

    return info;
}

let validateDataEmployee = () => {
    var info = {};
    info.FirstName = $('#FirstName').val();
    if (!info.FirstName) {
        console.log('NO VALUE HERE BRO');
        $('#FirstName').addClass('is-invalid');
        $('#error-messages-emp').html('All fields are required.');
    }

    info.LastName = $('#LastName').val();
    if (!info.LastName) {
        console.log('NO VALUE HERE BRO');
        $('#LastName').addClass('is-invalid');
        $('#error-messages-emp').html('All fields are required.');
    }

    info.Email = $('#Email').val();
    if (!info.Email) {
        console.log('NO VALUE HERE BRO');
        $('#Email').addClass('is-invalid');
        $('#error-messages-emp').html('All fields are required.');
    }
    info.SlackId = $('#SlackId').val();
    if (!info.SlackId) {
        console.log('NO VALUE HERE BRO');
        $('#SlackId').addClass('is-invalid');
        $('#error-messages-emp').html('All fields are required.');
    }
    return info;
}

let submitContactForm = (form, contactFormData) => {
    console.log(contactFormData);
    let contactFormEndpoint = 'http://' + window.location.host + '/api/contact/submit' + form;
    console.log(contactFormEndpoint);
    if (!contactFormData) {
        alert('GO AWAY!!')
    } else {
        $.ajax({
            type: 'POST',
            url: contactFormEndpoint,
            data: contactFormData,
            success: function (data, textStatus) { console.log(textStatus); location.reload(); },
            error: function (xhr, textStatus, errorThrown) { console.log('failed: ' + errorThrown); }
        });
    }
}

$.connection.alexaHub.client.submitForm = (form) => {
    console.log('I should submit now!... BUT WAIT I NEED TO VALIDATE STUFF I THINK');
    switch (form) {
        case 'project':
            let infoProject = validateDataProject();
            submitContactForm(form, infoProject);
            break;
        case 'employee':
            let infoEmployee = validateDataEmployee();
            submitContactForm(form, infoEmployee);
            break;
    }
}

$.connection.alexaHub.client.updateFormField = (fieldName, value) => {
    switch (fieldName) {
        case 'projectName':
            updateProjectNameInput(value);
            break;
        case 'firstName': 
            updateFirstNameInput(value);
            break;
        case 'lastName':
            updateLastNameInput(value);
            break;
        case 'email':
            updateEmailInput(value);
            break;
        case 'slackId':
            updateSlackIdInput(value);
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
