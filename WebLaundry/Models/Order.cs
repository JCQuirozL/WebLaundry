using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class Order
    {
        public long OrderId { get; set; }

        [Display(Name = "Fecha de ingreso")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Fecha de pago")]
        public DateTime? PayDate { get; set; }

        [Display(Name = "Cambio de estado")]
        public DateTime? StatusChangeDate { get; set; }

        [Display(Name = "Observaciones")]
        public string? Annotations { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
        public long? CustomerId { get; set; }
        public int? UserId { get; set; }
        public long? OrderDetailId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual OrderStatus? Status { get; set; }
    }
}
