using System;
///author Kristine Johnson
///
namespace com.GreenThumb.BusinessLogic.Interfaces
{
   public interface IGroupManager
    {
        System.Collections.Generic.List<com.GreenThumb.BusinessObjects.Group> GetGroupList(int OrganizationID);
    }
}
