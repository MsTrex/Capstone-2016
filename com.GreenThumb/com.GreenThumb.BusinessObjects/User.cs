// Updated by Poonam Dubey on 02/27/2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class User
    {
        /// <summary>
        /// Author: Poonam
        /// Data Transfer Object to represent a User from the
        /// Database
        /// 
        /// Added 3/3 By Trevor Glisch
        /// </summary>
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public int? RegionId { get; set; }

        public User() { }
        public User(int userID,
                     string firstName,
                     string lastName,
                     string zip,
                     string emailAddress,
                     string userName,
                     string password,
                     bool active,
                     int regionId)
        {
            UserID = userID;
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
            Active = active;
            RegionId = regionId;
        }
             

    }
}
