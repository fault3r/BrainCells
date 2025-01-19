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

function ToggleMenu()
{
    const divMenu = document.getElementById('menu-div');
    if (divMenu.style.width === '230px') {
        divMenu.style.width = '0px';
    } else {
        divMenu.style.width = '230px';
    }
}