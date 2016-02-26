using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Template
    {
        public int TemplateID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }

        public Template() { }

        public Template(int templateID, int userID, string description, DateTime dateCreated, bool active)
        {
            TemplateID = templateID;
            UserID = userID;
            Description = description;
            DateCreated = dateCreated;
            Active = active;
        }
    }
}
