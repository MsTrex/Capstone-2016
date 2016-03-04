///<summary>
///Author: Chris Schwebach
///Soil Donations Interface for the Buisness Logic Layer
///</summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    public interface ISoilDonationManager
    {
		///<summary>
		///Gets Soil Donations List
		///@returns: Soil Donations List
		///</summary>
		IEnumerable<SoilDonated> GetSoilDonationList(string stateLocated);
		 
		void AddSoilDonation(IEnumerable<SoilDonated> soilDonated, User user);
		
		///<summary>
		///Edits Soil Donation
		///@returns: true/false
		///</summary>
		bool EditSoilDonation(IEnumerable<SoilDonated> soilDonated, User user);
	}
}	