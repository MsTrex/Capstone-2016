﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Nutrient
    {
        public int NutrientID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Nutrient() { }

        public Nutrient(int nutrientID, string name, string description, string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate)
        {
            NutrientID = nutrientID;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            ModifiedBy = modifiedBy;
            ModifiedDate = modifiedDate;
        }
    }
}
