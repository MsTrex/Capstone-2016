using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
   public class DonationManager
    {
        public int AddVolunteerHours(DateTime startTime, DateTime finishTime, DateTime datePledged, int p)
        {
            try
           {
                int timePledge = TimePledgedAccessor.PledgeVolunteerHours(startTime, finishTime, datePledged, p);
                return timePledge;
           }
            catch (Exception)
           {

                throw;
           }
        }

       
    }
}
