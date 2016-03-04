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
    /// Manager class for Garden by Poonam Dubey
    /// </summary>
    public class GardenManager : IGardenManager
    {
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
    }
}
