
function showPassword() {
    const inpPassword = document.getElementById('Password');
    const inpCPassword = document.getElementById('ConfirmPassword');
    inpPassword.type = 'text';
    inpCPassword.type = 'text';
}

function hidePassword() {
    const inpPassword = document.getElementById('Password');
    const inpCPassword = document.getElementById('ConfirmPassword');
    inpPassword.type = 'password';
    inpCPassword.type = 'password';
}

function hideMessage() {
    const divMessage = document.getElementById('divMessage');
    divMessage.style.display = 'none';
}