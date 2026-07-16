document.addEventListener("click", async function (e) {

    // ================= Add =================

    const addBtn = e.target.closest(".add-wishlist");

    if (addBtn) {

        e.preventDefault();

        const productId = addBtn.dataset.id;

        const response = await fetch("/Wishlist/Add", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "X-Requested-With": "XMLHttpRequest"
            },
            body: `productId=${productId}`
        });

        if (response.status === 401) {
            window.location.href = "/Identity/Account/Login";
            return;
        }

        const result = await response.json();

        if (result.success) {
            addBtn.classList.add("active");
            document.getElementById("wishlist-count").textContent = result.count;
        }

        showToast(result.message);
        return;
    }

    // ================= Remove =================

    const removeBtn = e.target.closest(".remove-wishlist");

    if (removeBtn) {

        e.preventDefault();

        const productId = removeBtn.dataset.id;

        const response = await fetch("/Wishlist/Remove", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: `productId=${productId}`
        });

        if (response.status === 401) {
            window.location.href = "/Identity/Account/Login";
            return;
        }

        const result = await response.json();

        if (result.success) {

            removeBtn.closest("tr").remove();
            document.getElementById("wishlist-count").textContent = result.count;
        }
        showToast(result.message);
    }

});