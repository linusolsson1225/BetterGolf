document.addEventListener('DOMContentLoaded', function () {

    // Uppdatera cart count
    async function updateCartCount() {
        try {
            const response = await fetch('/ShoppingCart/GetCartCount', { credentials: 'same-origin' });
            const count = await response.json();
            const cartCountEl = document.getElementById('cart-count');
            if (cartCountEl) cartCountEl.innerText = count;
        } catch (error) {
            console.error('Error fetching cart count:', error);
        }
    }

    // Lägg till i cart
    document.body.addEventListener('click', async function (e) {
        if (!e.target.classList.contains('add-to-cart')) return;

        const btn = e.target;
        const productId = btn.dataset.id;
        const quantity = btn.dataset.quantity || 1;
        const variantId = btn.dataset.variantId || null;

        if (btn.disabled) return;

        try {
            const params = new URLSearchParams({ productId, quantity });
            if (variantId) params.append('variantId', variantId);

            const response = await fetch('/ShoppingCart/AddToCart', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: params.toString(),
                credentials: 'same-origin'
            });

            const html = await response.text();
            const cartBody = document.getElementById('cart-body-placeholder');
            if (cartBody) cartBody.innerHTML = html;

            await updateCartCount();
        } catch (err) {
            console.error('Failed to add item:', err);
        }
    });

    // Quantity buttons (+ / -)
    document.body.addEventListener('click', async function (e) {
        if (!e.target.classList.contains('quantity-btn')) return;

        const btn = e.target;
        const productId = btn.dataset.productId;
        const variantId = btn.dataset.variantId || null;
        const action = btn.dataset.action;

        try {
            const params = new URLSearchParams({ productId, action });
            if (variantId) params.append('variantId', variantId);

            const response = await fetch('/ShoppingCart/UpdateQuantity', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: params.toString(),
                credentials: 'same-origin'
            });

            const html = await response.text();
            const cartBody = document.getElementById('cart-body-placeholder');
            if (cartBody) cartBody.innerHTML = html;

            await updateCartCount();
        } catch (err) {
            console.error('Failed to update quantity:', err);
        }
    });

    // Thumbnail image switcher
    const thumbnails = document.querySelectorAll(".thumbnail-img");
    const mainImage = document.getElementById("mainProductImage");

    if (thumbnails.length && mainImage) {
        thumbnails[0].classList.add("active");

        thumbnails.forEach(img => {
            img.addEventListener("click", () => {
                mainImage.src = img.src;
                thumbnails.forEach(t => t.classList.remove("active"));
                img.classList.add("active");
            });
        });
    }

    // Variantselect + stock badge
    const variantSelect = document.getElementById("variantSelect");
    const addToCartBtn = document.querySelector(".add-to-cart");
    const stockBadge = document.getElementById("stockBadge");

    if (variantSelect && addToCartBtn && stockBadge) {
        const updateAddButton = () => {
            const variantId = variantSelect.value;

            if (!variantId) {
                stockBadge.style.display = "none";
                addToCartBtn.disabled = true;
                delete addToCartBtn.dataset.variantId;
                return;
            }

            const option = variantSelect.options[variantSelect.selectedIndex];
            const stock = parseInt(option.getAttribute("data-stock") || "0");

            addToCartBtn.dataset.variantId = variantId;
            stockBadge.style.display = "inline-block";

            if (stock > 0) {
                stockBadge.textContent = "In Stock";
                stockBadge.className = "badge bg-success";
                addToCartBtn.disabled = false;
            } else {
                stockBadge.textContent = "Out of Stock";
                stockBadge.className = "badge bg-danger";
                addToCartBtn.disabled = true;
            }
        };

        updateAddButton();
        variantSelect.addEventListener("change", updateAddButton);
    } else if (addToCartBtn) {
        addToCartBtn.disabled = false;
    }

    // Initiera cart count direkt vid load
    updateCartCount();
});
