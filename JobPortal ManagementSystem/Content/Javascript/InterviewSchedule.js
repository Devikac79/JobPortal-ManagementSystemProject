function validateLocation() {
    const locationField = document.getElementById('location').value.trim();
    if (locationField.length === 0) {
        showValidationMessage('location', 'Location is required.');
    } else {
        showValidationMessage('location', '');
    }
}

// Function to disable previous dates in the interview date field
// Function to disable previous dates in the interview date field on load time
function disablePreviousDates() {
    const postDateField = document.getElementById('interviewDate');
    const today = new Date().toISOString().split('T')[0];
    postDateField.setAttribute('min', today);
}

// Call the disablePreviousDates function on load time to initialize the postDate field with the minimum date restriction.
disablePreviousDates();
