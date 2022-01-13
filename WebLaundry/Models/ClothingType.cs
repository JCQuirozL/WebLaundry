using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class ClothingType
    {
        public ClothingType()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ClothingTypeId { get; set; }

        [Required(ErrorMessage ="Escribe un tipo de prenda.")]
        [Display(Name = "Tipo de Servicio")]
        public string? Name { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
