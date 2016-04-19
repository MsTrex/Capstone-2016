using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    public class GroupLeaderRequestManager
    {
        private AccessToken _accToken = null;
        private List<Group> _userGroups = null;
        public GroupLeaderRequestManager(AccessToken accToken)
        {
            this._accToken = accToken;
        }

        public List<Group> GetUserGroups()
        {
            _userGroups = DataAccess.GroupAccessor.RetrieveUsersGroups(_accToken.UserID, Active.active);

            return _userGroups;
        }

        public string AddGroupLeaderRequest(string groupName)
        {
            string returned = "";
            int count = 0;

            if(groupName != "")
            {
                //finds the group in the list that matches the selected group
                Group groupSelected = _userGroups.Where(x => x.Name.Equals(groupName)).First();
                
                try
                {
                    count = DataAccess.GroupAccessor.CreateGroupLeaderRequest(_accToken.UserID, groupSelected.GroupID, DateTime.Now);
                }
                catch (Exception)
                {

                    returned = "Sorry your request didn't go through, please try again";
                }

            }
            if (count == 1)
            {
                returned = "Your request was submited successfully";
            }
            else
            {
                returned = "Sorry your request didn't go through, please try again";
            }

            return returned;
        } 
    }

   
}
