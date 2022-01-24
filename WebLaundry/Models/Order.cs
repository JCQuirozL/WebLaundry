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
        public DateTime? CreateDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? StatusChangeDate { get; set; }
        public string? Annotations { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Total { get; set; }
        public int? StatusId { get; set; }
        public long? CustomerId { get; set; }
        public int? UserId { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }
        public int? ClothingId { get; set; }

        public decimal? Quantity { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual OrderStatus? Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
