function validateName() {
    var nameInput = document.getElementById("name");
    var name = nameInput.value.trim();
    var nameValidation = document.getElementById("nameValidation");

    if (name === "") {
        nameValidation.textContent = "Name cannot be empty";
        nameValidation.style.display = "block";
        return false;
    } else if (!/^[A-Za-z]+$/.test(name)) {
        nameValidation.textContent = "Name should only contain alphabets";
        nameValidation.style.display = "block";
        return false;
    }

    nameValidation.style.display = "none";
    return true;
}

function validateEmail() {
    var emailInput = document.getElementById("email");
    var email = emailInput.value.trim();
    var emailValidation = document.getElementById("emailValidation");

    if (email === "") {
        emailValidation.textContent = "Email cannot be empty";
        emailValidation.style.display = "block";
        return false;
    } else if (!/^[\w-\.]+@@([\w-]+\.)+[\w-]{2,4}$/.test(email)) {
        emailValidation.textContent = "Invalid email format";
        emailValidation.style.display = "block";
        return false;
    }

    emailValidation.style.display = "none";
    return true;
}

function validateSubject() {
    var subjectInput = document.getElementById("subject");
    var subject = subjectInput.value.trim();
    var subjectValidation = document.getElementById("subjectValidation");

    if (subject === "") {
        subjectValidation.textContent = "Subject cannot be empty";
        subjectValidation.style.display = "block";
        return false;
    }

    subjectValidation.style.display = "none";
    return true;
}

function validateMessage() {
    var messageInput = document.getElementById("message");
    var message = messageInput.value.trim();
    var messageValidation = document.getElementById("messageValidation");

    if (message === "") {
        messageValidation.textContent = "Message cannot be empty";
        messageValidation.style.display = "block";
        return false;
    }

    messageValidation.style.display = "none";
    return true;
}

// Attach the validation functions to the onfocusout event of their respective input fields
document.getElementById("name").addEventListener("focusout", validateName);
document.getElementById("email").addEventListener("focusout", validateEmail);
document.getElementById("subject").addEventListener("focusout", validateSubject);
document.getElementById("message").addEventListener("focusout", validateMessage);

function validateContactForm() {
    if (validateName() && validateEmail() && validateSubject() && validateMessage()) {
        alert("Form submitted successfully!");
        return true;
    } else {
        return false;
    }
}

// Attach the form validation function to the form's submit event
document.getElementById("contactForm").onsubmit = function () {
    return validateContactForm();
};