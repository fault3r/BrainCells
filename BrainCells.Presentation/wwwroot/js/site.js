function showPassword() {
    const inpPassword = document.getElementById('Password');
    inpPassword.type = 'text';
}

function hidePassword() {
    const inpPassword = document.getElementById('Password');
    inpPassword.type = 'password';
}

function hideMessage() {
    const divMessage = document.getElementById('divMessage');
    divMessage.style.display = 'none';
}