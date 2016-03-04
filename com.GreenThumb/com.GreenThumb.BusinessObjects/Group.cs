using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class Group
    {
        public int GroupID { get; set; }
        //public int Name { get; set; }//Kristine Johnson removed this by commenting it out, it should be string-see below
        public int GroupLeaderID { get; set; }
        public bool Active { get; set; }
        public List<User> UserList { get; set; }
        public DateTime CreatedDate { get; set; }


        ///Kristine Johnson Added

           
        public int OrganizationID { get; set; }
        public string Name { get; set; }
       
    
    }

}

