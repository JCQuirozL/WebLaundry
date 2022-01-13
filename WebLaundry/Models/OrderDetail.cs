using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Orders = new HashSet<Order>();
        }

        public long OrderDetailId { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números.")]
        [Required(ErrorMessage = "La cantidad no puede quedar en blanco.")]
        [Display(Name = "Cantidad")]
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
        public int? ServiceId { get; set; }
        public int? ClothingTypeId { get; set; }

        public virtual ClothingType? ClothingType { get; set; }
        public virtual ServiceType? Service { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
