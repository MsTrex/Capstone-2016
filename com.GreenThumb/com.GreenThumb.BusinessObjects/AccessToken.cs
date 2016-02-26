using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    public sealed class AccessToken : User
    {
        

        public AccessToken() { }
        public AccessToken(User user)
        {
            if (user == null || !user.Active)
            {
                throw new ApplicationException("Invalid User");
            }
            base.FirstName = user.FirstName;
            base.LastName = user.LastName;
            base.Zip = user.Zip;
            base.EmailAddress = user.EmailAddress;
            base.RegionId = user.RegionId;
            base.UserName = user.UserName;
            base.Active = user.Active;


        }


    }
}
