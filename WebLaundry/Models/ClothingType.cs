using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLaundry.Models
{
    public partial class ClothingType
    {
        public ClothingType()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ClothingTypeId { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        public string? Name { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
