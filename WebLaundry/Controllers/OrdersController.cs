using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebLaundry.Data;
using WebLaundry.Models;
using WebLaundry.Repositories;


namespace WebLaundry.Controllers
{
    public class OrdersController : Controller
    {
        private readonly laundryContext _context;
        private readonly IServiceTypeRepository serviceTypeRepository;

        public OrdersController(laundryContext context, 
                                IServiceTypeRepository serviceTypeRepository)
        {
            this.serviceTypeRepository = serviceTypeRepository;
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetServiceTypes()
        {
            List<ServiceType> serviceTypes = new List<ServiceType>();

            serviceTypes = _context.ServiceTypes.OrderBy(a => a.Name).ToList();

            return Json(new { serviceTypes , success = true});
        }

        public async Task<ActionResult> GetClothingTypes(int serviceTypeId)
        {
            List<ClothingType> clothingTypes = new List<ClothingType>();

            clothingTypes = await _context.ClothingTypes.Where(a => a.ServiceTypeId.Equals(serviceTypeId)).OrderBy(a => a.Name).ToListAsync();


            return Json(new { Data = clothingTypes }, new JsonSerializerSettings());


        }


        [HttpPost("/Orders/GetPrice")]
        public async Task<ActionResult> GetPrice(int clothingType)
        {
            var precio = (from ClothingType in _context.ClothingTypes
                          where ClothingType.ClothingTypeId == clothingType
                          select ClothingType.Price);

            return Json(precio);
        }


        [HttpPost]
        public async Task<ActionResult> Save(Order Order)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                _context.Orders.Add(Order);
                _context.SaveChanges();
                status = true;
            }
            return Json(new {Data = new { status = status}}, new JsonSerializerSettings());
        }


        [HttpGet]
        public async Task<ActionResult> Create(int? id)
        {
            var db = _context.OrderStatuses;
            var model = new Order
            {
                ServiceTypes = this.serviceTypeRepository.GetComboServiceTypes(),
                ClothingTypes = this.serviceTypeRepository.GetComboClothingTypes(0)

            };

                ViewBag.Customers = _context.Customers.Select(c => new {CustomerId = c.CustomerId, Name = c.Name}).ToList();
            
                ViewBag.Status = _context.OrderStatuses.Select(s => new { id = s.StatusId, name = s.Name }).ToList();

            if (id == null)
            {

                return View(model);
            }
            else
            {

                model = await _context.Orders.FindAsync(Convert.ToInt64(id));
                return View(model);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var orderDetail = new OrderDetail();
                var clothingtype = await serviceTypeRepository.GetClothingTypesAsync(order.ClothingId);
                if (order.OrderId == 0)
                {
                    orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ClothingTypeId = order.ClothingId,
                        ClothingType = clothingtype

                    };
                    await _context.OrderDetails.AddAsync(orderDetail);
                    await _context.SaveChangesAsync();

                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }

            return View(order);

        }

        [HttpPost("/Orders/GetClothingTypesAsync")]

        public async Task<JsonResult> GetClothingTypesAsync(int serviceTypeId)
        {
            var serviceType = await serviceTypeRepository.GetServiceTypeWithClothingTypesAsync(serviceTypeId);

            if (serviceType == null)
            {
                return Json("");
            } 


            return Json(serviceType.ClothingTypes.OrderBy(st => st.Name));
        }

       
    }
}
