    function validateUsername() {
        var usernameInput = document.getElementById("username");
        var username = usernameInput.value.trim();
        var usernameValidation = document.getElementById("usernameValidation");

        if (username === "") {
            usernameValidation.textContent = "Username cannot be empty";
            usernameValidation.style.display = "block";
            return false;
        } else if (!/^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$/.test(username)) {
            usernameValidation.textContent = "Invalid email format";
            usernameValidation.style.display = "block";
            return false;
        }

        usernameValidation.style.display = "none";
        return true;
    }

        function validatePassword() {
            var passwordInput = document.getElementById("password");
            var password = passwordInput.value.trim();
            var passwordValidation = document.getElementById("passwordValidation");

            if (password.length < 3) {
                passwordValidation.textContent = "Password should be valid";
                passwordValidation.style.display = "block";
                return false;
            }

            passwordValidation.style.display = "none";
            return true;
        }

document.getElementById("username").addEventListener("focusout", validateUsername);
document.getElementById("password").addEventListener("focusout", validatePassword);

document.getElementById("signinForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent default form submission

    // Validate username and password fields
    if (!validateUsername() || !validatePassword()) {
        alert("Please correct the validation errors before submitting.");
        return;
    }

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Home/Signin", true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.success) {
                    if (response.role === "user") {
                        alert("Login successful as user.");
                        window.location.href = "/User/UserHomepage"; // Redirect to user homepage
                    } else if (response.role === "admin") {
                        alert("Login successful as admin.");
                        window.location.href = "/Admin/AdminHomepage"; // Redirect to admin homepage
                    }
                } else {
                    alert("Invalid username or password.");
                }
            } else {
                alert("An error occurred during login.");
            }
        }
    };

    var data = "username=" + encodeURIComponent(username) + "&password=" + encodeURIComponent(password);
    xhr.send(data);
});
