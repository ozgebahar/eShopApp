using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Models
{
    public class CategoryViewModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girilebilir.")]
        public string CategoryName { get; set; }



        [Required(ErrorMessage = "Zorunlu alan")]
        [StringLength(500, ErrorMessage = "En fazla 500 karakter girilebilir.")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Zorunlu alan")]
        public IFormFile Picture { get; set; }

        public string PictureStr { get; set; }
        public bool IsActive { get; set; }
    }
}

