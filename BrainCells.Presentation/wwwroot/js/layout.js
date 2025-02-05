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
        if (divMenu.style.height === '130px') {
            divMenu.style.height = '0px';
        } else {
            divMenu.style.height = '130px';
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