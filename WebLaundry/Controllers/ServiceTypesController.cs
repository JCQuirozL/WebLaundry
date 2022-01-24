#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLaundry.Data;
using WebLaundry.Models;

namespace WebLaundry.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly laundryContext _context;

        public ServiceTypesController(laundryContext context)
        {
            _context = context;
        }

        //Get ServiceTypes
        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var db = _context.ServiceTypes;
            var lst = await (from ServiceType in db
                             select new
                             {
                                 typeid = ServiceType.ServiceTypeId,
                                 name = ServiceType.Name,
                               
                             }).ToListAsync();

            return Json(new { data = lst });

        }


        [HttpGet]
        // GET: ClothingTypesController/Create
        public async Task<ActionResult> Create(int? id)
        {
            ServiceType servicetypes = new();

            if (id == null)
            {

                return View(servicetypes);
            }
            else
            {

                servicetypes = await _context.ServiceTypes.FindAsync(id);
                return View(servicetypes);
            }
        }

        // POST: ClothingTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ServiceType serviceType)
        {

            if (ModelState.IsValid)
            {

                if (serviceType.ServiceTypeId == 0) //Crear registro
                {
                    await _context.ServiceTypes.AddAsync(serviceType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.ServiceTypes.Update(serviceType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }


            return View(serviceType);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var serviceType = await _context.ServiceTypes.FindAsync(id);
            if (serviceType == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Tipo de servicio borrado con éxito" });
        }
    }
}
