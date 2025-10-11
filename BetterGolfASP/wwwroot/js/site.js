async function updateCartCount() {
    try {
        const response = await fetch('/ShoppingCart/GetCartCount');
        const count = await response.json();
        document.getElementById('cart-count').innerText = count;
    } catch (error) {
        console.error('Error fetching cart count:', error);
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Add to cart
    document.querySelectorAll('.add-to-cart').forEach(button => {
        button.addEventListener('click', async function () {
            const productId = this.dataset.id;
            const quantity = this.dataset.quantity || 1;
            const name = this.dataset.name;
            const price = this.dataset.price;
            const imageUrl = this.dataset.imageUrl;

            try {
                const response = await fetch('/ShoppingCart/AddToCart', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: `productId=${productId}&quantity=${quantity}`
                });

                const html = await response.text();
                document.getElementById('cart-body-placeholder').innerHTML = html;
                await updateCartCount();
            } catch (error) {
                console.error('Failed to add item:', error);
            }
        });
    });

    // Remove from cart
    document.body.addEventListener('click', async function (e) {
        if (e.target.classList.contains('remove-from-cart')) {
            const productId = e.target.dataset.id;

            try {
                const response = await fetch('/ShoppingCart/RemoveFromCart', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: `productId=${productId}`
                });
                const html = await response.text();
                document.getElementById('cart-body-placeholder').innerHTML = html;
                await updateCartCount();
            } catch (error) {
                console.error('Failed to remove item:', error);
            }
        }
    });

    // Load cart when opening offcanvas
    const cartOffcanvas = document.getElementById('cartOffcanvas');
    cartOffcanvas.addEventListener('show.bs.offcanvas', async function () {
        try {
            const response = await fetch('/ShoppingCart/GetCartHtml');
            const html = await response.text();
            document.getElementById('cart-body-placeholder').innerHTML = html;
        } catch {
            document.getElementById('cart-body-placeholder').innerHTML = '<p class="text-danger">Could not load cart.</p>';
        }
    });

    updateCartCount();
});
document.body.addEventListener('click', async function (e) {
    if (e.target.classList.contains('cart-quantity-btn')) {
        const productId = e.target.dataset.id;
        const action = e.target.dataset.action;

        try {
            const response = await fetch('/ShoppingCart/UpdateQuantity', {
                method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                body: `productId=${productId}&action=${action}`
            });
            const html = await response.text();
            document.getElementById('cart-body-placeholder').innerHTML = html;

            // Uppdatera cart count också
            const countResponse = await fetch('/ShoppingCart/GetCartCount');
            const count = await countResponse.json();
            document.getElementById('cart-count').innerText = count;
        } catch (error) {
            console.error('Error updating quantity:', error);
        }
    }
});

