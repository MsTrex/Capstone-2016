///<summary>
///Author: Chris Schwebach
///Supply Needs Interface for the Buisness Logic Layer
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
    public interface ISupplyNeedManager
    {
		///<summary>
		///Gets Supply Needs List
		///@returns: Supply Needs List
		///</summary>
		IEnumerable<SupplyNeeded> GetSupplyNeedList(string stateLocated, Group group);
		 
		void AddSupplyNeed(IEnumerable<SupplyNeeded> supplyNeeded, User user, Group group);
		
		///<summary>
		///Edits Supply Need
		///@returns: true/false
		///</summary>
		bool EditSupplyNeed(IEnumerable<SupplyNeeded> supplyNeeded, User user, Group group);
	}
}