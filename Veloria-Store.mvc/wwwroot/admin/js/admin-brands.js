const modal = document.getElementById("brand-modal");

function openAddBrandModal() {

    document.getElementById("brand-form").reset();

    document.getElementById("brand-id").value = "";

    document.getElementById("modal-title").innerText = "Add Brand";

    modal.classList.add("open");
}

function closeBrandModal() {

    modal.classList.remove("open");
}

window.onclick = function (e) {

    if (e.target === modal)
        closeBrandModal();

};

document.addEventListener("keydown", function (e) {

    if (e.key === "Escape")
        closeBrandModal();

});

document
    .getElementById("brand-form")
    .addEventListener("submit", saveBrand);

async function saveBrand(e) {

    e.preventDefault();

    const id = document.getElementById("brand-id").value;

    const formData = new FormData();

    formData.append("Name", document.getElementById("brand-name").value);

    if (id) {
        formData.append("Id", id);
    }

    const url = id
        ? "/Admin/Brands/Update"
        : "/Admin/Brands/Create";

    const response = await fetch(url, {
        method: "POST",
        body: formData
    });

    const result = await response.json();

    if (!result.success) {
        showToast(result.message);
        return;
    }

    closeBrandModal();

    await loadBrands();

    showToast(result.message);

    document.getElementById("brand-form").reset();
}

async function editBrand(id) {

    const response =
        await fetch(`/Admin/Brands/Get?id=${id}`);

    const brand =
        await response.json();

    document.getElementById("brand-id").value =
        brand.id;

    document.getElementById("brand-name").value =
        brand.name;

    document.getElementById("modal-title").innerText =
        "Edit Brand";

    modal.classList.add("open");

}

async function deleteBrand(id) {

    if (!confirm("Delete this brand?"))
        return;

    const response = await fetch(`/Admin/Brands/Delete?id=${id}`, {

        method: "POST"

    });

    const result = await response.json();


    if (result.success) {

        loadBrands();
        showToast(result.message);


    }

}

async function loadBrands() {

    const response = await fetch("/Admin/Brands/List?" + new Date().getTime(), {
        cache: "no-store"
    });

    const brands = await response.json();

    renderBrands(brands);
}

function renderBrands(brands) {

    const tbody =
        document.getElementById("brands-table-body");

    tbody.innerHTML = "";

    if (brands.length === 0) {

        tbody.innerHTML =

            `<tr>
            <td colspan="3" style="text-align:center;padding:20px;">
                No Brands Found
            </td>
        </tr>`;

        return;
    }

    brands.forEach(brand => {

        tbody.innerHTML += `

        <tr>

            <td>
                <strong>${brand.name}</strong>
            </td>

            <td>

                <button class="btn btn-sm btn-primary"
                        onclick="editBrand('${brand.id}')">

                    Edit

                </button>

                <button class="btn btn-sm btn-danger"
                        onclick="deleteBrand('${brand.id}')">

                    Delete

                </button>

            </td>

        </tr>`;

    });

}

document.addEventListener("DOMContentLoaded", function () {

    loadBrands();

});