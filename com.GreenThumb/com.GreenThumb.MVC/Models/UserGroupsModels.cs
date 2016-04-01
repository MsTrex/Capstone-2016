using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    /// <summary>
    /// ViewModel for a user viewing basic information about a group.
    /// 
    /// Created By: Trent Cullinan 02/31/2016
    /// </summary>
    public class UserGroupViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string LeaderUserName { get; set; }
        public string LeaderEmail { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}