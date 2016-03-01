///<summary>
///Author: Chris Schwebach
///Supply Donations Interface for the Buisness Logic Layer
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
    public interface ISupplyDonationManager
    {
		///<summary>
		///Gets Supply Donations List
		///@returns: Supply Donations List
		///</summary>
		IEnumerable<SupplyDonated> GetSupplyDonationsList(string stateLocated);
		 
		void AddNewSupplyDonation(IEnumerable<SupplyDonated> supplyDonated, User user);
		
		///<summary>
		///Edits Supply Donation
		///@returns: true/false
		///</summary>
		bool EditSupplyDonation(IEnumerable<SupplyDonated> supplyDonated, User user);
	}
}	