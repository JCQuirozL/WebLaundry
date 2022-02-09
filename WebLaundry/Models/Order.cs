using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLaundry.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public long OrderId { get; set; }

        public DateTime? CreateDate
        {
            get
            {
                return this.createDate.HasValue
                    ? this.createDate.Value
                    : DateTime.Now;
            }

            set { this.createDate = value; }
        }

        public DateTime? createDate = null;
        public DateTime? PayDate { get; set; }
        public DateTime? StatusChangeDate { get; set; }


        [Display(Name = "Observaciones")]
        public string? Annotations { get; set; }

        public decimal? Subtotal { get; set; }


        public decimal? Iva
        {
            get
            {
                return this.iva.HasValue
                    ? this.iva.Value
                    : Convert.ToDecimal(1.16);
            }

            set { this.iva = value; }
        }
        public decimal? iva { get; set; }
        public decimal? Total { get; set; }

        [Display(Name = "Estado de la órden:")]
        public int? StatusId { get; set; }

        [Display(Name = "Cliente")]
        public long? CustomerId { get; set; }
        public int? UserId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Precio")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de prenda")]
        [Display(Name = "Tipo de Prenda")]
        public int ClothingId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ClothingTypes { get; set; }


        [Required(ErrorMessage = "Debe seleccionar un tipo de servicio")]
        [Display(Name = "Tipo de Servicio")]
        public int ServiceId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ServiceTypes { get; set; }


        [Required(ErrorMessage = "La cantidad es requerida")]
        [Display(Name = "Cantidad")]
        public decimal? Quantity { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual OrderStatus? Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
