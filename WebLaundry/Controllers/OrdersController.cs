using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task <ActionResult> getServiceTypes()
        {
            List<ServiceType> serviceTypes = new  List<ServiceType>();

            serviceTypes = await _context.ServiceTypes.OrderBy(a => a.Name).ToListAsync();

            return   Json (new { data = serviceTypes }, new JsonSerializerSettings() );
        }
        
        public async Task<ActionResult> getClothingTypes(int serviceTypeId)
        {
            List<ClothingType> clothingTypes = new List<ClothingType>();

            clothingTypes = await _context.ClothingTypes.Where(a => a.ServiceTypeId.Equals(serviceTypeId)).OrderBy(a => a.Name).ToListAsync();


            return Json(new { data = clothingTypes }, new JsonSerializerSettings());


        }


        public async Task<ActionResult> getPrices(int clothingType)
        {
            var precio = (from ClothingType in _context.ClothingTypes
                          select new
                          {

                           });
            return Json(new { data = precio }, new JsonSerializerSettings());
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
