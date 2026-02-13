// wwwroot/js/navbarToggler.js

/**
 * Initializes listeners for the navbar toggler to add/remove a class on collapse events.
 * @param {HTMLElement} togglerElement The button element reference from Blazor.
 * @param {string} collapseId The ID of the collapsible element (navRes).
 * @param {string} openClass The CSS class name to add when open.
 */
export function initializeToggler(togglerElement, collapseId, openClass) {
    const collapseElement = document.getElementById(collapseId);

    if (togglerElement && collapseElement) {
        // Use Bootstrap's native JS events for a clean implementation
        collapseElement.addEventListener('show.bs.collapse', function () {
            togglerElement.classList.add(openClass);
        });

        collapseElement.addEventListener('hide.bs.collapse', function () {
            togglerElement.classList.remove(openClass);
        });
    }
}

export function hideMenu(targetId) {
    const menu = document.getElementById(targetId);
    if (menu && menu.classList.contains('show')) {
        const bsCollapse = bootstrap.Collapse.getOrCreateInstance(menu);
        bsCollapse.hide();
    }
}
