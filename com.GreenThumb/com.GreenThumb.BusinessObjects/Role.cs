using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Role
    {
        public string RoleID { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Role(string roleID, string description, int createdBy, DateTime createdDate)
        {
            this.RoleID = roleID;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
        }

        public Role()
        {
        }
    }
}
