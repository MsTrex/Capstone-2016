using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    /// <summary>
    /// Created By: Luke Frahm 4/22/2016
    /// AnnouncementManager Interface for the Buisness Logic Layer
    /// </summary>
    public interface IAnnouncementManager
    {
        bool CreateAnnouncement(Announcements announcement);
        List<Announcements> GetAnnouncementsByGroupID(int groupID);
        List<Announcements> GetAnnouncementsByGroupIDTop10(int userID);
        bool UpdateAnnouncement(Announcements announcement);
    }
}
