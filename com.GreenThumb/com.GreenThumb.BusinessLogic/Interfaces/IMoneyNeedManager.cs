///<summary>
///Author: Chris Schwebach
///Money Needs Interface for the Buisness Logic Layer
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
    public interface IMoneyNeedManager
    {
		///<summary>
		///Gets Money Needs List
		///@returns: Money Needs List
		///</summary>
		IEnumerable<MoneyNeeded> GetMoneyNeedList(Group group);
		 
		void AddNewMoneyNeed(IEnumerable<MoneyNeeded> moneyNeeded, User user, Group group);
		
		///<summary>
		///Edits Money Needs
		///@returns: true/false
		///</summary>
		bool EditMoneyNeed(IEnumerable<MoneyNeeded> moneyNeeded, User user, Group group);	
	}
}