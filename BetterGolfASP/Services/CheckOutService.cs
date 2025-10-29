using BetterGolfASP.DB;
using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Models;

namespace BetterGolfASP.Services
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
            var customer = await FindByEmailAsync(email) ?? throw new InvalidOperationException($"No customer found with email '{email}'.");
            return customer;
        }
        //public async Task CreateNewCustomerAsync(string firstname, string lastname, string email)
        //{
        //    var existingCustomer = await FindByEmailAsync(email);
        //    if (existingCustomer != null)
        //        throw new InvalidOperationException("Customer already exists");

        //    var newCustomer = Customer.Create(firstname, lastname, email);
        //    _unitOfWork.CustomerRepository.Add(newCustomer);
        //    await _unitOfWork.SaveChangesAsync();
        //}
    }
}
