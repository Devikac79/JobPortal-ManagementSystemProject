
    // Function to validate the username (with email validation)
function validateUsername()
        {
        var usernameInput = document.getElementById("username");
        var username = usernameInput.value.trim();
        var usernameValidation = document.getElementById("usernameValidation");

        if (username === "")
        {
            usernameValidation.textContent = "Username cannot be empty";
            usernameValidation.style.display = "block";
            return false;
        }
        else if (!/^[\w-\.]+@@([\w-]+\.)+[\w-]{2, 4}$/.test(username))
        {
            usernameValidation.textContent = "Invalid email format";
            usernameValidation.style.display = "block";
            return false;
        }

        usernameValidation.style.display = "none";
        return true;
 }

    // Function to validate the password (minimum 3 characters)
function validatePassword()
    {
        var passwordInput = document.getElementById("password");
        var password = passwordInput.value.trim();
        var passwordValidation = document.getElementById("passwordValidation");

        if (password.length < 3)
        {
            passwordValidation.textContent = "Password should be at least 3 characters";
            passwordValidation.style.display = "block";
            return false;
        }

        passwordValidation.style.display = "none";
        return true;
 }

    // Attach the validation functions to the onfocusout event of their respective input fields
 document.getElementById("username").addEventListener("focusout", validateUsername);
 document.getElementById("password").addEventListener("focusout", validatePassword);

    // Function to validate the Signin form on submission
    function validateSigninForm() {
        return validateUsername() && validatePassword();
    }

    // Attach the form validation function to the form's submit event
    document.getElementById("signinForm").addEventListener("submit", function (event) {
        if (!validateSigninForm()) {
        event.preventDefault(); // Prevent form submission if validation fails
        }
    });
