using BetterGolfASP.DB;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BetterGolfASP.Controllers
{
    namespace BetterGolfASP.Controllers
    {
        public class OrderController : Controller   
        {
            private readonly UoW unitOfWork;

            public OrderController(Context context)
            {
                unitOfWork = new UoW(context);
            }

            public async Task<IActionResult> Index()  
            {
                var orders = await unitOfWork.OrderRepository.GetAllAsync();
                return View(orders);
            }

            public async Task<IActionResult> ChangeOrderStatus(int orderID, string orderStatus)
            {
                var order = await unitOfWork.OrderRepository.GetByIdAsync(orderID);
                order.Status = orderStatus;
                await unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }

}
