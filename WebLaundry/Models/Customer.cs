using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLaundry.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public long CustomerId { get; set; }

        [Required(ErrorMessage ="El apellido es requerido")]
        public string? Lastname { get; set; }


        [Required(ErrorMessage = "El nombre es requerido")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El domicilio es requerido")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "El teléfono es requerido")]
        public string? Phone { get; set; }


        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
