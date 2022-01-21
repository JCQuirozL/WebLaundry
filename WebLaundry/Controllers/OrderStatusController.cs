#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLaundry.Data;
using WebLaundry.Models;

namespace WebLaundry.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly laundryContext _context;

        public OrderStatusController(laundryContext context)
        {
            _context = context;
        }

        //Get OrderStatuses
        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var db = _context.OrderStatuses;
            var lst = await (from OrderStatus in db
                             select new
                             {
                                 clothingtypeid = OrderStatus.StatusId,
                                 name = OrderStatus.Name
                             }).ToListAsync();

            return Json(new { data = lst });

        }


        [HttpGet]
        // GET: OrderStatusController/Create
        public async Task<ActionResult> Create(int? id)
        {
            OrderStatus orderstatus = new();

            if (id == null)
            {

                return View(orderstatus);
            }
            else
            {

                orderstatus = await _context.OrderStatuses.FindAsync(id);
                return View(orderstatus);
            }
        }

        // POST: ClothingTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderStatus orderstatus)
        {

            if (ModelState.IsValid)
            {

                if (orderstatus.StatusId == 0) //Crear registro
                {
                    await _context.OrderStatuses.AddAsync(orderstatus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.OrderStatuses.Update(orderstatus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }


            return View(orderstatus);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderstatus = await _context.OrderStatuses.FindAsync(id);
            if (orderstatus == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.OrderStatuses.Remove(orderstatus);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Estado borrado con éxito" });
        }
    }
}
