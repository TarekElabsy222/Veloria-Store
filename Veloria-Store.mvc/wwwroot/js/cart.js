document.addEventListener("click", async function (e) {

    // =============================
    // Add To Cart
    // =============================

    let btn = e.target.closest(".add-cart");

    if (btn) {

        e.preventDefault();

        const result = await sendRequest("/Cart/Add", btn.dataset.id);

        if (result.success) {

            updateHeaderCount(result.count);

            updateCartTotals(result);

            showToast(result.message);
        }

        return;
    }

    // =============================
    // Remove
    // =============================

    btn = e.target.closest(".remove-cart");

    if (btn) {

        e.preventDefault();

        const row = btn.closest("tr");

        const result = await sendRequest("/Cart/Remove", btn.dataset.id);

        if (result.success) {

            row.remove();

            updateHeaderCount(result.count);

            updateCartTotals(result);

            showToast(result.message);
        }

        return;
    }

    // =============================
    // Increase Quantity
    // =============================

    btn = e.target.closest(".btn-cart-plus");

    if (btn) {

        e.preventDefault();

        const row = btn.closest("tr");

        const result = await sendRequest("/Cart/Increase", btn.dataset.id);

        if (result.success) {

            updateRow(row, result);

            updateHeaderCount(result.count);

            updateCartTotals(result);
        }

        return;
    }

    // =============================
    // Decrease Quantity
    // =============================

    btn = e.target.closest(".btn-cart-minus");

    if (btn) {

        e.preventDefault();

        const row = btn.closest("tr");

        const result = await sendRequest("/Cart/Decrease", btn.dataset.id);

        if (result.success) {

            if (result.removed) {

                row.remove();

            } else {

                updateRow(row, result);

            }

            updateHeaderCount(result.count);

            updateCartTotals(result);
        }

        return;
    }

});


// =====================================
// Common Request
// =====================================

async function sendRequest(url, productId) {

    try {

        const response = await fetch(url, {

            method: "POST",

            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },

            body: `productId=${productId}`

        });

        return await response.json();

    }
    catch {

        showToast("Something went wrong.", false);

        return { success: false };
    }

}


// =====================================
// Update Header Count
// =====================================

function updateHeaderCount(count) {

    const element = document.querySelector(".header-cart-count");

    if (element)
        element.textContent = count;

}


// =====================================
// Update Cart Totals
// =====================================

function updateCartTotals(result) {

    const subtotal = document.getElementById("cart-subtotal");

    if (subtotal)
        subtotal.textContent = "$" + result.cartSubtotal;

    const total = document.getElementById("cart-total");

    if (total)
        total.textContent = "$" + result.total;

}


// =====================================
// Update Product Row
// =====================================

function updateRow(row, result) {

    const quantity = row.querySelector(".quantity");

    if (quantity)
        quantity.value = result.quantity;

    const subtotal = row.querySelector(".subtotal");

    if (subtotal)
        subtotal.textContent = "$" + result.subtotal;

}