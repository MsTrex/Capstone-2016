using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Seed : Item
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Basic Data Transfer Object to represent Seeds from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Seed(int id, string name,
            int userID, bool active)
            : base (id, name)
        {
            this.UserID = userID;
            this.Active = active;
        }
    }
}
