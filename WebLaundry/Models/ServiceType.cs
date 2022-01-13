using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Captura el nombre del servicio.")]
        [Display(Name = "Nombre")]
        public string? Name { get; set; }

       
        [Required(ErrorMessage = "El precio no puede quedar en blanco.")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números.")]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
