///<summary>
///Author: Chris Schwebach
///Seed Donations Interface for the Buisness Logic Layer
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
    public interface ISeedDonationManager
    {
		///<summary>
		///Gets Seed Donations List
		///@returns: Seed Donations List
		///</summary>
		IEnumerable<SeedDonated> GetSeedDonationList(string stateLocated);
		 
		void AddSeedDonation(IEnumerable<SeedDonated> seedDonated, User user);
		
		///<summary>
		///Edits Seed Donation
		///@returns: true/false
		///</summary>
		bool EditSeedDonation(IEnumerable<SeedDonated> seedDonated, User user);
	}
}