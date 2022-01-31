using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLaundry.Data;
using WebLaundry.Models;

namespace WebLaundry.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly laundryContext context;

        public ServiceTypeRepository(laundryContext context)
        {
            this.context = context;
        }

        public IEnumerable<SelectListItem> GetComboServiceTypes()
        {
            var list = this.context.ServiceTypes.Select(st => new SelectListItem
            {
                Text = st.Name,
                Value = st.ServiceTypeId.ToString()
            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un tipo de Servicio",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboClothingTypes(int serviceTypeId)
        {
            var serviceType = this.context.ServiceTypes.Find(serviceTypeId);
            var list = new List<SelectListItem>();

            if (serviceType != null)
            {
                list = serviceType.ClothingTypes.Select(ct => new SelectListItem
                {
                    Text = ct.Name,
                    Value = ct.ClothingTypeId.ToString()
                }).OrderBy(l => l.Text).ToList();
            }

            list.Insert(0, new SelectListItem
            {
                Text = "Sleccione Tipo de Prenda",
                Value = "0"
            });

            return list;
        }

        public async Task<ServiceType> GetServiceTypeAsync(ClothingType clothingType)
        {
            return await context.ServiceTypes.Where(st => st.ClothingTypes
                                             .Any(ct => ct.ClothingTypeId == clothingType.ClothingTypeId))
                                             .FirstOrDefaultAsync();
        }

        public async Task<ClothingType> GetClothingTypesAsync(int id)
        {
            return await context.ClothingTypes.FindAsync(id);
        }

        public async Task<ServiceType> GetServiceTypeWithClothingTypesAsync(int id)
        {
            return await context.ServiceTypes
                .Include(st => st.ClothingTypes)
                .Where(st => st.ServiceTypeId == id)
                .FirstOrDefaultAsync();
                
        }

       
    }
}
