#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLaundry.Data;
using WebLaundry.Models;

namespace WebLaundry.Controllers
{
    public class ClothingTypesController : Controller
    {
        private readonly laundryContext _context;

        public ClothingTypesController(laundryContext context)
        {
            _context = context;
        }

        //Get ClothingTypes
        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var db = _context.ClothingTypes;
            var lst = await (from ClothingType in db
                             select new
                             {
                                 clothingtypeid = ClothingType.ClothingTypeId,
                                 name = ClothingType.Name,
                                 price = ClothingType.Price
                             }).ToListAsync();

            return Json(new { data = lst });

        }


        [HttpGet]
        // GET: ClothingTypesController/Create
        public async Task<ActionResult> Create(int? id)
        {
            ClothingType clothingType = new();

            if (id == null)
            {

                return View(clothingType);
            }
            else
            {

                clothingType = await _context.ClothingTypes.FindAsync(id);
                return View(clothingType);
            }
        }

        // POST: ClothingTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClothingType clothingType)
        {

            if (ModelState.IsValid)
            {

                if (clothingType.ClothingTypeId == 0) //Crear registro
                {
                    await _context.ClothingTypes.AddAsync(clothingType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.ClothingTypes.Update(clothingType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }


            return View(clothingType);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var clothingType = await _context.ClothingTypes.FindAsync(id);
            if (clothingType == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.ClothingTypes.Remove(clothingType);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Tipo de prenda borrado con éxito" });
        }


       
    }
}
