using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Domain.Models;
using BetterGolfASP.Domain.Models.Products;
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Presentation.ViewModels;


namespace BetterGolfASP.Application.Services
{
    public class CheckOutService(ILogger<CheckOutService> logger, Context context)
    {
        private readonly UoW _unitOfWork = new(context);
        
        
        public async Task<Customer?> FindByEmailAsync(string email)
        {
            return await _unitOfWork.CustomerRepository.GetByEmailAsync(email);
        }
        // Creates a new customer from the checkout data, saves it to the database, and logs the action.
        // Returns the newly created Customer object.
        private async Task<Customer> AddNewCustomerAsync(CheckoutViewModel model)
        {
            var customer = Customer.Create(
                model.FirstName,
                model.LastName,
                model.Email,
                model.Country,
                model.City,
                model.ZipCode,
                model.PhoneNumber
            );

            _unitOfWork.CustomerRepository.Add(customer);
            await _unitOfWork.SaveChangesAsync();
            logger.LogInformation("New customer added: {Email}", model.Email);

            return customer;
        }
        
        // Converts cart items into order rows by fetching the corresponding products and creating OrderRow objects.
        private async Task<List<OrderRow>> CreateOrderRowsAsync(IEnumerable<CartItem> cartItems)
        {
            var orderRows = new List<OrderRow>();

            foreach (var cartItem in cartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(cartItem.ProductId)
                              ?? throw new KeyNotFoundException($"Product with ID {cartItem.ProductId} not found.");
                var orderRow = OrderRow.Create(product, cartItem.Quantity,product.Variants.FirstOrDefault(v=>v.VariantId==cartItem.VariantId));
                orderRows.Add(orderRow);
            }

            return orderRows;
        }
        
        // Creates an OrderConfirmationViewModel from the provided order ID and checkout data.
        // Calculates the total amount and includes customer and cart item details.
        private Task<OrderConfirmationViewModel> CreateOrderConfirmationViewModel(int orderId,
            CheckoutViewModel model)
        {
            var orderConfirmationViewModel = new OrderConfirmationViewModel()
            {
                OrderNumber = orderId.ToString(),
                TotalAmount = model.CartItems.Sum(i => i.Price * i.Quantity),
                OrderItems = model.CartItems,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Country = model.Country,
                City = model.City,
                ZipCode = model.ZipCode,
            };
            return Task.FromResult(orderConfirmationViewModel);
        }
        
        // Reduces the stock of all products and variants included in the given order.
        // Logs the start and completion of the stock reduction process.
        private async Task ReduceStockForOrderAsync(Order order)
        {
            logger.LogInformation("Reducing stock for order {OrderId}", order.OrderId);

            foreach (var row in order.OrderRows)
            {
                await ReduceStockForProductAsync(row.ProductId, row.Quantity, row.VariantId);
            }

            logger.LogInformation("Stock reduced for order {OrderId}", order.OrderId);
        }
        
        // High-level method that handles stock reduction for a product or variant
        private async Task ReduceStockForProductAsync(int productId, int quantity, int? variantId = null)
        {
            var product = await GetProductAsync(productId);
            ValidateStock(product, quantity, variantId);
            ApplyStockReduction(product, quantity, variantId);
            await _unitOfWork.SaveChangesAsync();
            logger.LogInformation("Reduced stock for product {ProductId} by {Quantity}", productId, quantity);
        }
        
        // Fetches the product from the repository
        private async Task<Product> GetProductAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            return product;
        }
        
        // Validates stock availability for the product or variant
        private void ValidateStock(Product product, int quantity, int? variantId)
        {
            if (product.HasVariants)
            {
                if (!variantId.HasValue)
                    throw new ArgumentException("Variant ID is required for products with variants.", nameof(variantId));

                var variant = product.Variants.FirstOrDefault(v => v.VariantId == variantId)
                              ?? throw new InvalidOperationException($"Variant with ID {variantId} not found.");

                if (variant.Stock < quantity)
                    throw new InvalidOperationException($"Not enough stock for {variant}. Current stock: {variant.Stock}");
            }
            else
            {
                if (!product.Stock.HasValue)
                    throw new InvalidOperationException($"Product {product.ProductId} has no stock value set.");

                if (product.Stock < quantity)
                    throw new InvalidOperationException($"Not enough stock for product {product.ProductId}. Current stock: {product.Stock}");
            }
        }
        
        // Applies the stock reduction to the product or variant
        private static void ApplyStockReduction(Product product, int quantity, int? variantId)
        {
            if (product.HasVariants)
            {
                var variant = product.Variants.First(v => variantId != null && v.VariantId == variantId.Value);
                variant.ReduceVariantStock(quantity);
            }
            else
            {
                product.ReduceStock(quantity);
            }
        }
        
        // Processes a full checkout: places the order, reduces stock for the ordered items,
        // and creates an order confirmation view model to return to the customer.
        public async Task<OrderConfirmationViewModel> ProcessOrderAsync(CheckoutViewModel model)
        {
            logger.LogInformation("Processing full checkout for {Email}", model.Email);
            var order = await PlaceOrderAsync(model);
            await ReduceStockForOrderAsync(order);
            var confirmationVm = CreateOrderConfirmationViewModel(order.OrderId, model);

            return await confirmationVm;
        }
        
        // Creates and saves a new order from the checkout data.
        // Finds or adds the customer, generates order rows from cart items, saves the order, and logs the process.
        private async Task<Order> PlaceOrderAsync(CheckoutViewModel model)
        {
            logger.LogInformation("Starting checkout for {Email}", model.Email);

            var customer = await FindByEmailAsync(model.Email)
                           ?? await AddNewCustomerAsync(model);

            var orderRows = await CreateOrderRowsAsync(model.CartItems);
            var order = Order.Create(customer, orderRows);

            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync();
            logger.LogInformation("Order placed successfully for customer {Email}", customer.Email);
            return order;
        }
    }
}
