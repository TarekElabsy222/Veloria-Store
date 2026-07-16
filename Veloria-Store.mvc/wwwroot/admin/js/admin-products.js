const modal = document.getElementById("product-modal");

function openAddProductModal() {

    document.getElementById("product-form").reset();

    document.getElementById("product-id").value = "";

    document.getElementById("modal-title").innerText = "Add Product";

    document.getElementById("image-preview").innerHTML = "";

    modal.classList.add("open");
}

function closeProductModal() {

    modal.classList.remove("open");
}

window.onclick = function (e) {

    if (e.target === modal)
        closeProductModal();

};

document.addEventListener("keydown", function (e) {

    if (e.key === "Escape")
        closeProductModal();

});

document.getElementById("product-images")
    .addEventListener("change", previewImages);

function previewImages() {

    const container =
        document.getElementById("image-preview");

    container.innerHTML = "";

    const files =
        this.files;

    for (let file of files) {

        const reader = new FileReader();

        reader.onload = function (e) {

            container.innerHTML +=

                `<img src="${e.target.result}"
                      class="image-preview">`;

        };

        reader.readAsDataURL(file);

    }

}

document
    .getElementById("product-form")
    .addEventListener("submit", saveProduct);

async function saveProduct(e) {

    e.preventDefault();

    const id =
        document.getElementById("product-id").value;

    const formData = new FormData();

    formData.append("Name",
        document.getElementById("product-name").value);

    formData.append("Description",
        document.getElementById("product-description").value);

    formData.append("Price",
        document.getElementById("product-price").value);

    formData.append("StockQuantity",
        document.getElementById("product-stock").value);

    formData.append("DiscountPercentage",
        document.getElementById("product-discount").value);

    formData.append("CategoryId",
        document.getElementById("product-category").value);

    formData.append("BrandId",
        document.getElementById("product-brand").value);

    formData.append("IsFeatured",
        document.getElementById("product-featured").checked);

    formData.append("IsPopular",
        document.getElementById("product-popular").checked);

    formData.append("IsTrendy",
        document.getElementById("product-trendy").checked);

    if (id)
        formData.append("Id", id);

    const files =
        document.getElementById("product-images").files;

    for (let i = 0; i < files.length; i++) {

        formData.append("Images", files[i]);

    }

    const url = id
        ? "/Admin/Products/Update"
        : "/Admin/Products/Create";

    const response = await fetch(url, {

        method: "POST",

        body: formData

    });

    const result = await response.json();


    if (result.success) {

        closeProductModal();

        await loadProducts();
        showToast(result.message);


    }

}

async function loadProducts() {

    const response =
        await fetch("/Admin/Products/List?" + new Date().getTime());

    const products =
        await response.json();

    renderProducts(products);

}

function renderProducts(products) {

    const tbody =
        document.getElementById("products-table-body");

    tbody.innerHTML = "";

    if (products.length === 0) {

        tbody.innerHTML =

            `<tr>

                <td colspan="6"
                    style="text-align:center;padding:20px;">

                    No Products Found

                </td>

            </tr>`;

        return;

    }

    products.forEach(product => {

        let image =
            "/images/no-image.png";

        if (product.images &&
            product.images.length > 0) {

            image = product.images[0];

        }

        tbody.innerHTML +=

            `<tr>

                <td>

                    <div style="display:flex;align-items:center;gap:15px;">

                        <img src="${image}"
                             style="width:55px;height:55px;border-radius:8px;object-fit:cover;">

                        <div>

                            <strong>${product.name}</strong>

                            <div style="font-size:13px;color:#888;">
                                ${product.description.substring(0, 40)}...
                            </div>

                        </div>

                    </div>

                </td>

                <td>

                    ${product.categoryName}

                </td>

                <td>

                    ${product.brandName}

                </td>

                <td>

                    $${product.price}

                </td>

                <td>

                    ${product.stockQuantity}

                </td>

                <td>

                    <button class="btn btn-primary btn-sm"
                            onclick="editProduct('${product.id}')">

                        Edit

                    </button>

                    <button class="btn btn-danger btn-sm"
                            onclick="deleteProduct('${product.id}')">

                        Delete

                    </button>

                </td>

            </tr>`;

    });

}

async function editProduct(id) {

    const response =
        await fetch(`/Admin/Products/Get?id=${id}`);

    const product =
        await response.json();

    document.getElementById("product-id").value =
        product.id;

    document.getElementById("product-name").value =
        product.name;

    document.getElementById("product-description").value =
        product.description;

    document.getElementById("product-price").value =
        product.price;

    document.getElementById("product-stock").value =
        product.stockQuantity;

    document.getElementById("product-discount").value =
        product.discountPercentage;

    document.getElementById("product-category").value =
        product.categoryId;

    document.getElementById("product-brand").value =
        product.brandId;

    document.getElementById("product-featured").checked =
        product.isFeatured;

    document.getElementById("product-popular").checked =
        product.isPopular;

    document.getElementById("product-trendy").checked =
        product.isTrendy;

    const preview =
        document.getElementById("image-preview");

    preview.innerHTML = "";

    if (product.images) {

        product.images.forEach(image => {

            preview.innerHTML +=

                `<img src="${image}" class="image-preview">`;

        });

        console.log(product);
        console.log(product.images);

    }

    document.getElementById("product-images").value = "";

    document.getElementById("modal-title").innerText =
        "Edit Product";

    modal.classList.add("open");

}




async function deleteProduct(id) {

    if (!confirm("Delete this product?"))
        return;

    const response =
        await fetch(`/Admin/Products/Delete?id=${id}`, {

            method: "POST"

        });

    const result =
        await response.json();


    if (result.success) {

        loadProducts();
        showToast(result.message);


    }

}

document.addEventListener("DOMContentLoaded", function () {

    loadProducts();

});