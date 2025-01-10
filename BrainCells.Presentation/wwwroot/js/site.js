function showPassword() {
    const inpPassword = document.getElementById('password');
    inpPassword.type = 'text';
}

function hidePassword() {
    const inpPassword = document.getElementById('password');
    inpPassword.type = 'password';
}

function showMessage() {
    const divMain = document.getElementById('divMain');
    const divMessage = document.getElementById('divMessage');
    divMain.style.zIndex = '0';
    divMessage.style.display = 'block';
}

function hideMessage() {
    const divMain = document.getElementById('divMain');
    const divMessage = document.getElementById('divMessage');
    divMain.style.zIndex = '2';
    divMessage.style.display = 'none';
}