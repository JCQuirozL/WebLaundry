using System;
using System.Collections.Generic;

namespace WebLaundry.Models
{
    public partial class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
        public long? OrderId { get; set; }
        public int? ClothingTypeId { get; set; }

        public virtual ClothingType? ClothingType { get; set; }
        public virtual Order? Order { get; set; }
    }
}
