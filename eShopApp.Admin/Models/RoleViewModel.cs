using eShopApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApp.Admin.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public bool IsActive { get; set; }
        public List<User> Users { get; set; }
    }
}
