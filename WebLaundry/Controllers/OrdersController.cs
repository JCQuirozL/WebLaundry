using Microsoft.AspNetCore.Mvc;
using WebLaundry.Data;
using WebLaundry.Models;

namespace WebLaundry.Controllers
{
    public class OrdersController : Controller
    {
        private readonly laundryContext _context;

        public OrdersController(laundryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Create(int? id)
        {
            var db = _context.OrderStatuses;
            Order order = new();
            ViewBag.Customers = _context.Customers.Select(c => new {CustomerId = c.CustomerId, Name = c.Name}).ToList();
            
            ViewBag.Status = _context.OrderStatuses.Select(s => new { id = s.StatusId, name = s.Name }).ToList();

            ViewBag.ClothingType = _context.ClothingTypes.Select(c => new {id = c.ClothingTypeId, name = c.Name, price = c.Price }).ToList();

            

            if (id == null)
            {

                return View(order);
            }
            else
            {

                order = await _context.Orders.FindAsync(Convert.ToInt64(id));
                return View(order);
            }

        }

    }
}
