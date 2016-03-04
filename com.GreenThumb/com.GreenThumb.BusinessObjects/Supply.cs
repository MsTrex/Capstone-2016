using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Supply : Item
    {
        /// <summary>
        /// Author: Ryan Taylor
        /// Data Transfer Object to represent Supplies from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int UserID { get; set; }
        public bool Active { get; set; }

        public Supply(int id, string name, int userID, bool active) : base(id, name)
        {
            this.UserID = userID;
            this.Active = active;
        }
    }
}
