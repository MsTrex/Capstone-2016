/*
* Author: Chris Schwebach
* Land Donations Interface for the Buisness Logic Layer
*
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.BusinessLogic.Interfaces
{
    public interface ILandDonationManager
    {
		///<summary>
		///Gets Land Donations List
		///@returns: Land Donations List
		///</summary>
		IEnumerable<LandDonated> GetLandDonationList(string city);
		 
		void AddLandDonation(IEnumerable<LandDonated> landDonated, User user);
		
		///<summary>
		///Edits Land Donation
		///@returns: true/false
		///</summary>
		bool EditLandDonation(IEnumerable<LandDonated> landDonated, User user);
	}
}	