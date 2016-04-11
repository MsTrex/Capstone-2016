using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    public class GardenCreationViewModel
    {
        [Required]
        [Display(Name = "Garden Description")]
        public string GardenDescription { get; set; }

        public int GroupID { get; set; }
        
    }
}