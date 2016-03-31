using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Added by Sara Nanke on 03/22/2016
    /// Blog object for the BlogEntry in the database
    /// </summary>
    public class Blog
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; } //added to database
        public string BlogData { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }

        public Blog() { }
        public Blog(int BlogID, string BlogTitle, string BlogData, int CreatedBy, int ModifiedBy, DateTime ModifiedDate, DateTime DateCreated, bool Active)
        {
            this.BlogID = BlogID;
            this.BlogTitle = BlogTitle;
            this.BlogData = BlogData;
            this.CreatedBy = CreatedBy;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedDate = ModifiedDate;
            this.DateCreated = DateCreated;
            this.Active = Active;
        }

    }
}


