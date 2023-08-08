// Function to show validation message
function showValidationMessage(inputId, message) {
    const validationSpan = document.getElementById(inputId + 'Validation');
    validationSpan.textContent = message;
    if (message) {
        validationSpan.style.display = "block";
    } else {
        validationSpan.style.display = "none";
    }
}

// Validation function for the first name field
function validateFirstName() {
    const firstNameField = document.getElementById('firstName').value.trim();
    if (firstNameField.length === 0) {
        showValidationMessage('firstName', 'First name cannot be empty.');
    } else {
        showValidationMessage('firstName', '');
    }
}

// Validation function for the last name field
function validateLastName() {
    const lastNameField = document.getElementById('lastName').value.trim();
    if (lastNameField.length === 0) {
        showValidationMessage('lastName', 'last name is required.');
    } else {
        showValidationMessage('lastName', '');
    }
}



// Validation function for the Date of Birth field
function validateDateOfBirth() {
    const dateOfBirthField = document.getElementById('dateOfBirth').value;
    const today = new Date();
    const inputDate = new Date(dateOfBirthField);

    // Calculate minimum date (18 years ago)
    const minDate = new Date();
    minDate.setFullYear(today.getFullYear() - 18);

    if (inputDate > today) {
        showValidationMessage('dateOfBirth', 'Date of birth cannot be a future date.');
    } else if (inputDate > minDate) {
        showValidationMessage('dateOfBirth', 'You must be at least 18 years old.');
    } else {
        showValidationMessage('dateOfBirth', '');
    }
}
// Disable future dates in the Date of Birth field when the form loads
document.addEventListener('DOMContentLoaded', function () {
    const dateOfBirthField = document.getElementById('dateOfBirth');
    const today = new Date().toISOString().split('T')[0];
    dateOfBirthField.setAttribute('max', today);
});

function validateEmail() {
    const emailField = document.getElementById('email').value.trim();
    const emailValidation = document.getElementById('emailValidation');

    const emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/; // Basic email format regex

    if (emailField === '') {
        emailValidation.textContent = 'Email cannot be empty.';
        emailValidation.style.display = 'block';
        return false;
    } else if (!emailRegex.test(emailField)) {
        emailValidation.textContent = 'Invalid email format.';
        emailValidation.style.display = 'block';
        return false;
    }

    emailValidation.style.display = 'none';
    return true;
}



function validatePhone() {
    const phoneField = document.getElementById('phone').value.trim();
    const phoneValidation = document.getElementById('phoneValidation');

    const phoneRegex = /^[0-9]{10}$/; // Basic 10-digit phone number format

    if (phoneField === '') {
        phoneValidation.textContent = 'Phone number cannot be empty.';
        phoneValidation.style.display = 'block';
        return false;
    } else if (!phoneRegex.test(phoneField)) {
        phoneValidation.textContent = 'Invalid phone number format.';
        phoneValidation.style.display = 'block';
        return false;
    }

    phoneValidation.style.display = 'none';
    return true;
}
function validateAddress() {
    const addressField = document.getElementById('address').value.trim();
    const addressValidation = document.getElementById('addressValidation');

    if (addressField === '') {
        addressValidation.textContent = 'Address cannot be empty.';
        addressValidation.style.display = 'block';
        return false;
    }

    addressValidation.style.display = 'none';
    return true;
}

function validateCountry() {
    const countryField = document.getElementById('country').value.trim();
    const countryValidation = document.getElementById('countryValidation');

    if (countryField === '') {
        countryValidation.textContent = 'Country cannot be empty.';
        countryValidation.style.display = 'block';
        return false;
    }

    countryValidation.style.display = 'none';
    return true;
}

function validateCity() {
    const cityField = document.getElementById('city').value.trim();
    const cityValidation = document.getElementById('cityValidation');

    if (cityField === '') {
        cityValidation.textContent = 'City cannot be empty.';
        cityValidation.style.display = 'block';
        return false;
    }

    cityValidation.style.display = 'none';
    return true;
}

function validateState() {
    const stateField = document.getElementById('state').value.trim();
    const stateValidation = document.getElementById('stateValidation');

    if (stateField === '') {
        stateValidation.textContent = 'State cannot be empty.';
        stateValidation.style.display = 'block';
        return false;
    }

    stateValidation.style.display = 'none';
    return true;
}
function validatePincode() {
    const pincodeField = document.getElementById('pincode').value.trim();
    const pincodeValidation = document.getElementById('pincodeValidation');

    if (pincodeField === '') {
        pincodeValidation.textContent = 'Pincode cannot be empty.';
        pincodeValidation.style.display = 'block';
        return false;
    } else if (!/^\d{6}$/.test(pincodeField)) {
        pincodeValidation.textContent = 'Pincode must be a 6-digit number.';
        pincodeValidation.style.display = 'block';
        return false;
    }

    pincodeValidation.style.display = 'none';
    return true;
}

function validateUsername() {
    const usernameField = document.getElementById('username').value.trim();
    const usernameValidation = document.getElementById('usernameValidation');

    const emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/; // Basic email format regex

    if (usernameField === '') {
        usernameValidation.textContent = 'Username cannot be empty.';
        usernameValidation.style.display = 'block';
        return false;
    } else if (!emailRegex.test(usernameField)) {
        usernameValidation.textContent = 'Invalid username format.';
        usernameValidation.style.display = 'block';
        return false;
    }

    usernameValidation.style.display = 'none';
    return true;
}

function validatePassword() {
    const passwordField = document.getElementById('password').value;
    const passwordValidation = document.getElementById('passwordValidation');

    if (passwordField === '') {
        passwordValidation.textContent = 'Password cannot be empty.';
        passwordValidation.style.display = 'block';
        return false;
    } else if (passwordField.length < 8) {
        passwordValidation.textContent = 'Password must be at least 8 characters.';
        passwordValidation.style.display = 'block';
        return false;
    }

    passwordValidation.style.display = 'none';
    return true;
}

function validateConfirmPassword() {
    const passwordField = document.getElementById('password').value;
    const confirmPasswordField = document.getElementById('confirmPassword').value;
    const confirmPasswordValidation = document.getElementById('confirmPasswordValidation');

    if (confirmPasswordField === '') {
        confirmPasswordValidation.textContent = 'Confirm Password cannot be empty.';
        confirmPasswordValidation.style.display = 'block';
        return false;
    } else if (passwordField !== confirmPasswordField) {
        confirmPasswordValidation.textContent = 'Passwords do not match.';
        confirmPasswordValidation.style.display = 'block';
        return false;
    }

    confirmPasswordValidation.style.display = 'none';
    return true;
}