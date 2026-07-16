const modal = document.getElementById("category-modal");

function openAddCategoryModal() {

    document.getElementById("category-form").reset();
    document.getElementById("cat-id").value = "";
    document.getElementById("modal-title").innerText = "Add Category";
    document.getElementById("image-preview").innerHTML = "";

    modal.classList.add("open");
}

function closeCategoryModal() {
    modal.classList.remove("open");
}

window.onclick = function (e) {
    if (e.target === modal) {
        closeCategoryModal();
    }
};

document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") {
        closeCategoryModal();
    }
});

document.getElementById("cat-image").addEventListener("change", function () {

    const file = this.files[0];

    if (!file)
        return;

    const reader = new FileReader();

    reader.onload = function (e) {

        document.getElementById("image-preview").innerHTML =
            `<img src="${e.target.result}" class="image-preview">`;

    };

    reader.readAsDataURL(file);

});

document.getElementById("category-form")
    .addEventListener("submit", saveCategory);

async function saveCategory(e) {

    e.preventDefault();

    const id = document.getElementById("cat-id").value;

    const formData = new FormData();

    formData.append("Name", document.getElementById("cat-name").value);

    if (id)
        formData.append("Id", id);

    const image = document.getElementById("cat-image").files[0];

    if (image)
        formData.append("Image", image);

    const url = id
        ? "/Admin/Categories/Update"
        : "/Admin/Categories/Create";

    try {

        const response = await fetch(url, {
            method: "POST",
            body: formData
        });

        if (!response.ok) {

            console.log(await response.text());

            return;
        }

        const result = await response.json();

        console.log(result);

        if (result.success) {

            closeCategoryModal();

            document.getElementById("category-form").reset();
            document.getElementById("image-preview").innerHTML = "";

            await loadCategories();

            showToast(result.message);
        }

    }
    catch (err) {

        console.error(err);

    }

}

async function editCategory(id) {

    const response = await fetch(`/Admin/Categories/Get?id=${id}`);

    const category = await response.json();

    document.getElementById("cat-id").value = category.id;
    document.getElementById("cat-name").value = category.name;

    document.getElementById("modal-title").innerText = "Edit Category";

    if (category.imageUrl) {

        document.getElementById("image-preview").innerHTML =
            `<img src="${category.imageUrl}" class="image-preview">`;

    }
    else {

        document.getElementById("image-preview").innerHTML = "";

    }

    document.getElementById("cat-image").value = "";

    modal.classList.add("open");

}

async function loadCategories() {

    const tbody = document.getElementById("categories-table-body");

    tbody.innerHTML =
        `<tr>
            <td colspan="4" style="text-align:center">
                Loading...
            </td>
        </tr>`;

    const response =
        await fetch("/Admin/Categories/List");

    const categories =
        await response.json();

    renderCategories(categories);

}
function renderCategories(categories) {

    const tbody = document.getElementById("categories-table-body");

    tbody.innerHTML = "";

    if (categories.length === 0) {

        tbody.innerHTML =
            `<tr>
                <td colspan="3" style="text-align:center;padding:20px;">
                    No Categories Found
                </td>
            </tr>`;

        return;
    }

    categories.forEach(category => {

        tbody.innerHTML += `
        <tr>

            <td>
                <div style="display:flex;align-items:center;gap:12px;">

                    <img src="${category.imageUrl ?? '/images/no-image.png'}"
                         style="width:50px;height:50px;border-radius:8px;object-fit:cover;">

                </div>
            </td>

            <td>${category.name}</td>

            <td>

                <button class="btn btn-sm btn-primary"
                        onclick="editCategory('${category.id}')">

                    Edit

                </button>

                <button class="btn btn-sm btn-danger"
                     onclick="deleteCategory('${category.id}')">
                 Delete
                 </button>

            </td>

        </tr>`;
    });

}

document.addEventListener("DOMContentLoaded", function () {

    loadCategories();

});


async function deleteCategory(id) {

    const confirmed = confirm("Are you sure you want to delete this category?");

    if (!confirmed)
        return;

    const response = await fetch("/Admin/Categories/Delete?id=" + id, {

        method: "POST"

    });

    const result = await response.json();

    if (result.success) {


        await loadCategories();
        showToast(result.message);


    }

}