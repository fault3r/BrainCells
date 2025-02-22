function toggleClass(element, className){
    if (!element || !className){
        return;
    }
    var classString = element.className;
    var nameIndex = classString.indexOf(className);
    if (nameIndex == -1) {
        classString += ' ' + className;
    } else {
        classString = classString.substr(0, nameIndex) + classString.substr(nameIndex+className.length);
    }
    element.className = classString;
}
    document.getElementById('btn-menu-responsive').addEventListener('click', function() {
        toggleClass(this, 'close');
    });

function ToggleMenu()
{
    const divMenu = document.getElementById('menu-div');
    if (divMenu.style.width === '230px') {
        divMenu.style.width = '0px';
    } else {
        divMenu.style.width = '230px';
    }
}
function ProfileMenu()
{
    const divMenu = document.getElementById('profile-div');
    if (divMenu.style.height === 'auto') {
        divMenu.style.height = '0px';
    } else {
        divMenu.style.height = 'auto';
    }
}

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

function faMessageClose() {
    const divMessage = document.getElementById('faMessage');
    divMessage.style.display = 'none';
}
window.addEventListener('scroll', function() {
    const documentHeight = document.documentElement.scrollHeight;
    const viewportHeight = window.innerHeight;
    const scrollPosition = window.scrollY;
    const maxScrollableHeight = documentHeight - viewportHeight;
    const widthPercentage = (scrollPosition / maxScrollableHeight) * 100;
    const progressBar = document.getElementById('progressBar');
    progressBar.style.width = widthPercentage + '%';
});

document.getElementById('upload-file').addEventListener('click', function() {
    document.getElementById('fileInput').click();
});
document.getElementById('fileInput').addEventListener('change', function(event) {
    const file = event.target.files[0]; // Get the selected file
    const imagePreview = document.getElementById('imagePreview');

    if (file) {
        if (file.type === 'image/jpeg') {
            const reader = new FileReader();
            reader.onload = function(e) {
                imagePreview.src = e.target.result;
            };
            reader.readAsDataURL(file);
        }
    }
});
document.getElementById('remove-file').addEventListener('click', function() {
    const imagePreview = document.getElementById('imagePreview');
    imagePreview.src = "/resource/profile-picture.png";
    const fileInput = document.getElementById('fileInput');
    fileInput.value = '';
});
