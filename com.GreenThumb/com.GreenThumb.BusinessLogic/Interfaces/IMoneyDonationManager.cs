///<summary>
///Author: Chris Schwebach
///Money Donations Interface for the Buisness Logic Layer
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
    public interface IMoneyDonationManager
    {
		///<summary>
		///Gets Money Donations List
		///@returns: Money Donations List
		///</summary>
		IEnumerable<MoneyDonated> GetMoneyDonationList();
		 
		void AddNewMoneyDonation(IEnumerable<MoneyDonated> moneyDonated, User user);
		
		///<summary>
		///Edits Money Donation
		///@returns: true/false
		///</summary>
		bool EditMoneyDonation(IEnumerable<MoneyDonated> moneyDonated, User user);
	}
}	