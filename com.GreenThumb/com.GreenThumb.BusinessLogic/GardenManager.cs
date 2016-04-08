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
    }
}
