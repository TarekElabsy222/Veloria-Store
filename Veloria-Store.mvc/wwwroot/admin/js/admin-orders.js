const modal = document.getElementById("order-modal");

let currentOrderId = null;

function getStatusBadge(status) {

    switch (status) {

        case "Pending":
            return `<span class="badge badge-warning">Pending</span>`;

        case "Processing":
            return `<span class="badge badge-info">Processing</span>`;

        case "Shipped":
            return `<span class="badge badge-primary">Shipped</span>`;

        case "Delivered":
            return `<span class="badge badge-success">Delivered</span>`;

        case "Cancelled":
            return `<span class="badge badge-danger">Cancelled</span>`;

        default:
            return `<span class="badge">${status}</span>`;
    }

}

function closeOrderModal() {

    modal.classList.remove("open");

}

window.addEventListener("click", function (e) {

    if (e.target === modal) {
        closeOrderModal();
    }

});

document.addEventListener("keydown", function (e) {

    if (e.key === "Escape") {
        closeOrderModal();
    }

});

async function loadOrders() {

    try {

        const response = await fetch("/Admin/Orders/List?ts=" + Date.now());

        const orders = await response.json();

        renderOrders(orders);

    }
    catch (error) {

        console.error(error);

    }

}

function renderOrders(orders) {

    const tbody = document.getElementById("orders-table-body");

    tbody.innerHTML = "";

    if (!orders || orders.length === 0) {

        tbody.innerHTML = `
            <tr>
                <td colspan="6" style="text-align:center;padding:20px;">
                    No Orders Found
                </td>
            </tr>`;

        return;

    }

    orders.forEach(order => {

        tbody.innerHTML += `
            <tr>

                <td>${order.orderNumber}</td>

                <td>${order.customerName}</td>

                <td>$${order.total}</td>

                <td>${getStatusBadge(order.statusName)}</td>

                <td>${new Date(order.createdAt).toLocaleDateString("en-GB")}</td>

                <td>

                    <button class="btn btn-sm btn-primary"
                            onclick="viewOrder('${order.id}')">

                        View

                    </button>

                </td>

            </tr>`;
    });

}

async function viewOrder(id) {

    currentOrderId = id;

    const response = await fetch(`/Admin/Orders/Get?id=${id}`);

    const order = await response.json();

    document.getElementById("order-number").value = order.orderNumber;
    document.getElementById("customer-name").value = order.customerName;
    document.getElementById("customer-email").value = order.email;
    document.getElementById("customer-phone").value = order.phone;
    document.getElementById("order-total").value = "$" + order.total;
    document.getElementById("order-status").value = order.status;

    const tbody = document.getElementById("order-items-body");

    tbody.innerHTML = "";

    order.items.forEach(item => {

        tbody.innerHTML += `
            <tr>

                <td>

                    <img src="${item.productImage}"
                         style="width:55px;height:55px;border-radius:8px;object-fit:cover;">

                </td>

                <td>${item.productName}</td>

                <td>$${item.price}</td>

                <td>${item.quantity}</td>

                <td>$${item.total}</td>

            </tr>`;

    });

    modal.classList.add("open");

}

async function saveOrderStatus() {

    try {

        const formData = new FormData();

        formData.append("Id", currentOrderId);
        formData.append("Status", document.getElementById("order-status").value);

        const response = await fetch("/Admin/Orders/UpdateStatus", {
            method: "POST",
            body: formData
        });

        const result = await response.json();

        if (!result.success) {

            showToast(result.message);
            return;

        }

        closeOrderModal();

        await loadOrders();

        showToast(result.message);

    }
    catch (error) {

        console.error(error);

        showToast("Something went wrong.");

    }

}

document.getElementById("order-search")
    .addEventListener("keyup", function () {

        const value = this.value.toLowerCase();

        document.querySelectorAll("#orders-table-body tr")
            .forEach(row => {

                row.style.display =
                    row.innerText.toLowerCase().includes(value)
                        ? ""
                        : "none";

            });

    });

document.addEventListener("DOMContentLoaded", function () {

    loadOrders();

});