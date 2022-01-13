using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int StatusId { get; set; }

        [Required(ErrorMessage = "Captura el nombre del estado.")]
        public string? Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
