//menu-close toggle button
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
// *END

// sidebar menus
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
// *END

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