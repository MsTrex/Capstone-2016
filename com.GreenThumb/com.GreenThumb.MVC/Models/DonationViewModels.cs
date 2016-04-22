using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/21/16
    /// </summary>
    public class CreateNeedViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int GardenId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "View Publicly?")]
        public bool ViewPublicly { get; set; }
    }

    /// <summary>
    /// 
    /// Created By: Trent Cullinan 04/21/16
    /// </summary>
    public class SendNeedViewModel
    {
        
    }
}