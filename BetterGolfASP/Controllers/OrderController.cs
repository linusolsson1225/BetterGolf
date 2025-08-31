using BetterGolfASP.DB;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BetterGolfASP.Controllers
{
    public class OrderController: Controller
    {
        UoW unitOfWork = new UoW();
        public void ChangeOrderStatus(int orderID, string orderStatus)
        {
            Order order = (unitOfWork.OrderRepository.GetById(orderID));
            order.Status = (orderStatus);
            unitOfWork.SaveChanges();
        }
    }
}
