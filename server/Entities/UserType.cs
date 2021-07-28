using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class UserType
    {
        public UserType()
        {
            DataUsers = new HashSet<DataUser>();
        }

        public byte UserTypeId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<DataUser> DataUsers { get; set; }
    }
}
