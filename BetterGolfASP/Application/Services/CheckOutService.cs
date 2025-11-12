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

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _unitOfWork.CustomerRepository.GetAllAsync();
        }

        public async Task<Customer?> FindByEmailAsync(string email)
        {
            return await _unitOfWork.CustomerRepository.GetByEmailAsync(email);
        }
        
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

        public Task<OrderConfirmationViewModel> CreateOrderConfirmationViewModel(int orderId,
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
        public async Task ReduceStockForOrderAsync(Order order)
        {
            logger.LogInformation("Reducing stock for order {OrderId}", order.OrderId);

            foreach (var row in order.OrderRows)
            {
                await ReduceStockAsync(row.ProductId, row.Quantity, row.VariantId);
            }

            logger.LogInformation("Stock reduced for order {OrderId}", order.OrderId);
        }
        
        private async Task ReduceStockAsync(int productId,int quantity, int? variantId=null)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId)
                          ?? throw new KeyNotFoundException($"Product with ID {productId} not found.");

            if (product.HasVariants)
            {
                if (!variantId.HasValue)
                    throw new ArgumentException("Variant ID is required for products with variants.", nameof(variantId));

                var variant = product.Variants.FirstOrDefault(v => v.VariantId == variantId)
                              ?? throw new InvalidOperationException($"Variant with ID {variantId} not found.");

                if (variant.Stock < quantity)
                    throw new InvalidOperationException($"Not enough stock for {variant}. Current stock: {variant.Stock}");

                variant.ReduceVariantStock(quantity);
            }
            else
            {
                if (!product.Stock.HasValue)
                    throw new InvalidOperationException($"Product {productId} has no stock value set.");

                if (product.Stock < quantity)
                    throw new InvalidOperationException($"Not enough stock for product {productId}. Current stock: {product.Stock}");

                product.ReduceStock(quantity);
            }

            await _unitOfWork.SaveChangesAsync();
            logger.LogInformation("Reduced stock for product {ProductId} by {Quantity}", productId, quantity);
        }

        public async Task<OrderConfirmationViewModel> ProcessOrderAsync(CheckoutViewModel model)
        {
            logger.LogInformation("Processing full checkout for {Email}", model.Email);
            var order = await PlaceOrderAsync(model);
            await ReduceStockForOrderAsync(order);
            var confirmationVm = CreateOrderConfirmationViewModel(order.OrderId, model);

            return await confirmationVm;
        }

        public async Task<Order> PlaceOrderAsync(CheckoutViewModel model)
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
