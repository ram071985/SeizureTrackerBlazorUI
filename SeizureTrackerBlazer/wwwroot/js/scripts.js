function showToast(toastElementId) {
    var toastEl = document.getElementById(toastElementId);
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}