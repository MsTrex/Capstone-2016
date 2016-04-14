using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.GreenThumb.MVC.Models
{
    public class GardenTaskViewModel
    {
        public IEnumerable<Group> GroupsList { get; set; }

        public IEnumerable<Job> JobList { get; set; }

        public Job JobObj { get; set; }
    }
}