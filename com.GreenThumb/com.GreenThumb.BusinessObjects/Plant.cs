using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Plant
    {

        public int PlantID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Season { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Plant(int PlantID, string Name, string Type, string Category, string Description, string Season,
                      int CreatedBy, DateTime CreatedDate, int ModifiedBy, DateTime ModifiedDate)
        {

            this.PlantID = PlantID;
            this.Name = Name;
            this.Type = Type;
            this.Category = Category;
            this.Description = Description;
            this.Season = Season;
            this.CreatedBy = CreatedBy;
            this.CreatedDate = CreatedDate;
            this.ModifiedBy = ModifiedBy;
            this.ModifiedDate = ModifiedDate;

        }

    }
}
