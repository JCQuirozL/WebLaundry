using Microsoft.AspNetCore.Mvc.Rendering;
using WebLaundry.Models;

namespace WebLaundry.Repositories
{
    public interface IServiceTypeRepository
    {
        IEnumerable<SelectListItem> GetComboServiceTypes();

        IEnumerable<SelectListItem> GetComboClothingTypes(int serviceTypeId);

        Task<ServiceType> GetServiceTypeAsync(ClothingType clothingType);

        Task<ClothingType> GetClothingTypesAsync(int clothingTypeId);

        Task<ServiceType> GetServiceTypeWithClothingTypesAsync(int clothingTypeId);

        //decimal GetPriceAsync(int clothingTypeId);

    }
}
