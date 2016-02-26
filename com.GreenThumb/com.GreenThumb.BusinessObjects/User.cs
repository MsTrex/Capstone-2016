using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RegionId { get; set; }
        public bool Active { get; set; }

        public User() { }

        public User(int userId,
                    string firstName,
                    string lastName,
                    int zip,
                    string emailAddress,
                    string userName,
                    string password,
                    string regionId,
                    bool active)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
            RegionId = regionId;
            Active = active;
        }
    }
}
