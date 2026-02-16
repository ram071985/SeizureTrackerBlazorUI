getUserTimeZone = () => {
    return Intl.DateTimeFormat().resolvedOptions().timeZone;
    // Returns "America/Chicago", "America/New_York", etc.
}

function showToast(toastElementId) {
    var toastEl = document.getElementById(toastElementId);
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}

function initializeNavbarToggler() {
    const toggler = document.getElementById('navTogglerBtn');
    const collapseElement = document.getElementById('navRes');

    if (toggler && collapseElement) {
        // Bootstrap provides events for when the collapse is shown/hidden
        collapseElement.addEventListener('show.bs.collapse', function () {
            toggler.classList.add('is-open');
            toggler.setAttribute('aria-expanded', 'true');
        });

        collapseElement.addEventListener('hide.bs.collapse', function () {
            toggler.classList.remove('is-open');
            toggler.setAttribute('aria-expanded', 'false');
        });
    }
}

// Call the function once the script is loaded
initializeNavbarToggler();