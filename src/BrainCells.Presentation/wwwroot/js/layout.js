

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
    document.getElementById('btn-menu').addEventListener('click', function() {
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
    const file = event.target.files[0];
    const imagePreview = document.getElementById('imagePreview');
    if (file) {
        if (file.type === 'image/jpeg' || file.type === 'image/png'){
            const reader = new FileReader();
            reader.onload = function(e) {
                imagePreview.style.backgroundImage = '';
                imagePreview.src = e.target.result;
            };
            reader.readAsDataURL(file);
            const defaultPicture = document.getElementById('DefaultPicture');
            defaultPicture.checked = false;
        }
        else {
            document.getElementById('fileInput').value = '';
        }
    }
});
function UploadButtonRemove(str) {
    const imagePreview = document.getElementById('imagePreview');
    imagePreview.src = "/resource/"+ str +".png";
    document.getElementById('fileInput').value = '';
    const defaultPicture = document.getElementById('DefaultPicture');
    defaultPicture.checked = true;
}

function showSearch(){
    const searchMenu = document.getElementById('search-container');
    searchMenu.style.display = 'block';  
    const pLine = document.getElementById('profile-line');
    pLine.style.display = 'none';
}
function hideContainers(){
    const searchMenu = document.getElementById('search-container');
    searchMenu.style.display = 'none';    
    const pLine = document.getElementById('profile-line');
    pLine.style.display = 'block';
    const divProfile = document.getElementById('profile-div');
    divProfile.style.height = '0px';
}