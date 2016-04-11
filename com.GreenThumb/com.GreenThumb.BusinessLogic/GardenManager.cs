using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    /// <summary>
    /// Manager class 
    /// Author: Poonam Dubey
    /// </summary>
    public class GardenManager : IGardenManager
    {
        /// <summary>
        /// Bool Method to create Garden by Poonam Dubey
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        public bool CreateGarden(Garden garden)
        {
            try
            {
                return GardenAccessor.CreateGarden(garden);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        ///Created by: Kristine Johnson
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="garden"></param>
        /// <returns></returns>
        public bool AddGarden(Garden garden)
        {
            try
            {
                return GardenAccessor.AddGarden(garden);
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
        }


        /// <summary>
        /// Manager function to fetch all gardens : Poonam Dubey  (20th March 2016)
        /// </summary> 
        /// <returns></returns>
        public List<Garden> GetGardens()
        {
            try
            {
                return GardenAccessor.GetGardens();
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Author: Chris Schwebch, Nick King
        /// Date: 04/6/16
        /// Gets gardens the user belongs to
        /// </summary> 
        public List<Group> GetGardenByUser(int userID)
        {
            try
            {
                return GardenAccessor.FetchGardenInfo(userID);
            }
            catch (Exception)
            {
                List<Group> groupList = new List<Group>();
                return groupList;
            }
        }

        /// <summary>
        /// Create a garden for a group without throwing exceptions to view.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="garden">Garden to create.</param>
        /// <returns>Whether the action was successful.</returns>
        public bool AddGroupGarden(Garden garden)
        {
            bool flag = false;

            try
            {
                flag = GardenAccessor.CreateGarden(garden);
            }
            catch (Exception) { } // flag set to false

            return flag;
        }

        /// <summary>
        /// Retrieve the gardens for a group.
        /// 
        /// Created by: Trent Cullinan 04/05/2016
        /// </summary>
        /// <param name="groupId">Identifier to retrieve gardens by.</param>
        /// <returns>Collection of gardens.</returns>
        public IEnumerable<Garden> RetrieveGroupGardens(int groupId)
        {
            IEnumerable<Garden> gardens = null;

            try
            {
                gardens = GardenAccessor.RetrieveGroupGardens(groupId);
            }
            catch (Exception) { } // collection will be null

            return gardens;
        }
    }
}
