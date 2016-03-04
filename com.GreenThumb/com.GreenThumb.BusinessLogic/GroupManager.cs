using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;

/// <summary>
/// Created by Kristine Johnson 2/28/16
/// Selects a list of groups within an organziation
/// </summary>

namespace com.GreenThumb.BusinessLogic
{
        public class GroupManager : com.GreenThumb.BusinessLogic.Interfaces.IGroupManager
    {
        public List<Group> GetGroupList(int OrganizationID)
        {
           
            try
            {
                return GroupAccessor.GetGroupList(OrganizationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }

    }
}
