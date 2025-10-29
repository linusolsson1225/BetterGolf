using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Domain.Models;
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Presentation.ViewModels;

namespace BetterGolfASP.Application.Services
{
    public class CheckOutService(ILogger<CheckOutService> logger, Context context)
    {
        private readonly ILogger<CheckOutService> _logger = logger;
        private readonly UoW _unitOfWork = new UoW(context);

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _unitOfWork.CustomerRepository.GetAllAsync();
        }

        public async Task<Customer?> FindByEmailAsync(string email)
        {
            return await _unitOfWork.CustomerRepository.GetByEmailAsync(email);
        }

        public async Task<Customer> GetRequiredByEmailAsync(string email)
        {
            var customer = await FindByEmailAsync(email) ??
                           throw new InvalidOperationException($"No customer found with email '{email}'.");
            return customer;
        }

        public async Task PlaceOrderAsync(CheckoutViewModel model)
        {
            var existingCustomer = await FindByEmailAsync(model.Email);
            Customer customer;

            if (existingCustomer == null)
            {
                customer = Customer.Create(
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
            }
            else
            {
                customer = existingCustomer;
            }
            var orderRows = new List<OrderRow>();
            foreach (var cartItem in model.CartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(cartItem.ProductID);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {cartItem.ProductID} not found.");

                var orderRow = OrderRow.Create(product, cartItem.Quantity);
                orderRows.Add(orderRow);
            }

            var order = Order.Create(customer, orderRows);
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}


