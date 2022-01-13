#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLaundry.Models;

namespace WebLaundry.Controllers
{
    public class CustomersController : Controller
    {
        private readonly LaundryContext _context;

        public CustomersController(LaundryContext context)
        {
            _context = context;
        }

        //Get Customers
        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var db = _context.Customers;
            var lst = await (from Customer in db
                             select new
                             {
                                 customerid = Customer.CustomerId,
                                 name = Customer.Name,
                                 lastname = Customer.Lastname,
                                 address = Customer.Address,
                                 phone = Customer.Phone,
                                 email = Customer.Email

                             }).ToListAsync();

            return Json(new { data = lst });

        }


        [HttpGet]
        // GET: CustomersController/Create
        public async Task<ActionResult> Create(int? id)
        {
            Customer customer = new();

            if (id == null)
            {

                return View(customer);
            }
            else
            {

                customer = await _context.Customers.FindAsync(Convert.ToInt64(id));
                return View(customer);
            }
        }

        // POST: cUSTOMERSController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Customer customer)
        {

            if (ModelState.IsValid)
            {

                if (customer.CustomerId == 0) //Crear registro
                {
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }


            return View(customer);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(Convert.ToInt64(id));
            if (customer == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Cliente borrado con éxito" });
        }


        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
