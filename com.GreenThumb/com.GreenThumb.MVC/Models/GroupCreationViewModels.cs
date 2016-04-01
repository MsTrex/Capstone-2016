using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace com.GreenThumb.MVC.Models
{
    public class GroupCreationViewModels
    {

        [Display(Name = "UserID")]
        int UserID { get; set; }

        [Display(Name = "UserName")]
        string UserName { get; set; }

        [Required]
        [Display(Name = "GroupName")]
        string GroupName { get; set; }

        [Display(Name = "GroupLeaderID")]
        int GroupLeaderID { get; set; }
    }
}