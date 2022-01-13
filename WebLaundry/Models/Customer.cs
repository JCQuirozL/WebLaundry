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

        [Required(ErrorMessage ="Debes capturar al menos un apellido para el cliente.")]
        [Display(Name = "Apellidos")]
        public string? Lastname { get; set; }
        
        
        [Required(ErrorMessage = "Debes capturar al menos un nombre para el cliente.")]
        [Display(Name = "Nombre")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Debes capturar el domicilio para el cliente.")]
        [Display(Name = "Domicilio")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Debes capturar el número de teléfono para el cliente.")]
        [Display(Name = "Teléfono")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Debes capturar el correo para el cliente.")]
        [Display(Name = "Correo")]
        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
