using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class GardenGuide
    {
        public int GardenGuideID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }

        public GardenGuide() { }

        public GardenGuide(int gardenGuideID, int userID, string content)
        {
            GardenGuideID = gardenGuideID;
            UserID = userID;
            Content = content;
        }
    }
}
