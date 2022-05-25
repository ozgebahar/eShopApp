using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girilebilir.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [EmailAddress(ErrorMessage = "ornek@mail.com şeklinde giriş yapınız.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(12, ErrorMessage = "En fazla 12 karakter girilebilir.")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string County { get; set; }

        [Required]
        [StringLength(50)]
        public string District { get; set; }

        [Required]
        [StringLength(100)]
        public string HomeAddress { get; set; }
    }
}
