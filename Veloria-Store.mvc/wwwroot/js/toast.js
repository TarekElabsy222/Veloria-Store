function showToast(message, success = true) {

    Toastify({

        text: message,

        duration: 2500,

        gravity: "top",

        position: "right",

        close: true,

        stopOnFocus: true,

        style: {
            background: success
                ? "linear-gradient(to right,#00b09b,#96c93d)"
                : "linear-gradient(to right,#ff416c,#ff4b2b)"
        }

    }).showToast();
}