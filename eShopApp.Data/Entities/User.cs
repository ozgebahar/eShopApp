using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace eShopApp.Data.Entities
{
    public class User : BaseEntity
    {

        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        [StringLength(12, MinimumLength = 4)]
        [Required]
        public string Password { get; set; }

        [StringLength(100)]
        [Required]
        public string Firstname { get; set; }

        [StringLength(100)]
        [Required]
        public string Lastname { get; set; }

        public UserRoleEnum UserRole { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }


    }
}
