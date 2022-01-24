using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId{ get; set; }

        [Required(ErrorMessage ="Escriba un nombre para el tipo de servicio")]
        public string? Name{ get; set; }

        public ICollection<ClothingType>? ClothingTypes { get; set; }
    }
}
