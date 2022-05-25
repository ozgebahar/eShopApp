using System;
using System.Collections.Generic;
using System.Text;

namespace eShopApp.Data.Entities
{
   public class Role : BaseEntity
    {
        
        public UserRoleEnum UserRole { get; set; }


        #region Relations

        public List<User> Users { get; set; }

        #endregion
    }
}
