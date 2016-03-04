using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Garden
    {
        /// <summary>
        /// Author: Luke Frahm
        /// Data Transfer Object to represent a Garden
        /// from the Database
        /// </summary>

        public int GardenID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string GardenDescription { get; set; }
        public string GardenRegion { get; set; }

        public Garden() { }

        public Garden(int gardenID, int groupID, int userID, string gardenDescription, string gardenRegion)
        {
            GardenID = gardenID;
            GroupID = groupID;
            UserID = userID;
            GardenDescription = gardenDescription;
            GardenRegion = gardenRegion;
        }
    }
}
