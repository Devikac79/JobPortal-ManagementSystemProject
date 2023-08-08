// Function to show validation message in the <span> element
function showValidationMessage(inputId, message) {
    const validationSpan = document.getElementById(inputId + 'ValidationMessage');
    validationSpan.textContent = message;
    validationSpan.style.display = message ? "block" : "none";
}

// Validation function for the title field
function validateTitle() {
    const titleField = document.getElementById('title').value.trim();
    if (titleField.length < 3) {
        showValidationMessage('title', 'Title is required.');
    } else {
        showValidationMessage('title', '');
    }
}

// Validation function for the companyName field
function validateCompanyName() {
    const companyNameField = document.getElementById('companyName').value.trim();
    if (companyNameField.length === 0) {
        showValidationMessage('companyName', 'Company name is required');
    } else {
        showValidationMessage('companyName', '');
    }
}

// Validation function for the location field
function validateLocation() {
    const locationField = document.getElementById('location').value.trim();
    if (locationField.length === 0) {
        showValidationMessage('location', 'Location is required.');
    } else {
        showValidationMessage('location', '');
    }
}

// Validation function for the minSalary field
function validateMinSalary() {
    const minSalaryField = document.getElementById('minSalary').value.trim();
    if (minSalaryField === '') {
        showValidationMessage('minSalary', 'Minimum salary cannot be empty.');
    } else {
        showValidationMessage('minSalary', '');
    }
}

// Validation function for the maxSalary field
function validateMaxSalary() {
    const maxSalaryField = document.getElementById('maxSalary').value.trim();
    if (maxSalaryField === '') {
        showValidationMessage('maxSalary', 'Maximum salary cannot be empty.');
    } else {
        showValidationMessage('maxSalary', '');
    }
}

// Function to disable previous dates in the postDate field
// Function to disable previous dates in the postDate field on load time
function disablePreviousDates() {
    const postDateField = document.getElementById('postDate');
    const today = new Date().toISOString().split('T')[0];
    postDateField.setAttribute('min', today);
}

// Call the disablePreviousDates function on load time to initialize the postDate field with the minimum date restriction.
disablePreviousDates();

// Validation function for the description field
function validateDescription() {
    const descriptionField = document.getElementById('description').value.trim();
    if (descriptionField.length < 10) {
        showValidationMessage('description', 'Description is required.');
    } else {
        showValidationMessage('description', '');
    }
}

// Validation function for the jobCategory field
function validateJobCategory() {
    const jobCategoryField = document.getElementById('jobCategory').value.trim();
    if (jobCategoryField.length < 3) {
        showValidationMessage('jobCategory', 'Category cannot be empty.');
    } else {
        showValidationMessage('jobCategory', '');
    }
}

// Validation function for the jobNature field
function validateJobNature() {
    const jobNatureField = document.getElementById('jobNature').value.trim();
    if (jobNatureField.length < 3) {
        showValidationMessage('jobNature', 'Nature cannot be empty.');
    } else {
        showValidationMessage('jobNature', '');
    }
}

// Additional validation function for the image field
function validateImage() {
    const imageField = document.getElementById('imageFile');
    const allowedFormats = ['image/jpeg', 'image/jpg'];
    const file = imageField.files[0];
    if (file) {
        const fileType = file.type;
        if (!allowedFormats.includes(fileType)) {
            showValidationMessage('image', 'Only JPG and JPEG formats are allowed.');
        } else {
            showValidationMessage('image', ''); // Clear the validation message if valid
        }
    }
}

// Add the validateImage function to the file input's onfocusout event
document.getElementById('imageFile').addEventListener('focusout', validateImage);
