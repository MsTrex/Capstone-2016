///<summary>
/// Author: Chris Schwebach
/// Soil Needs Interface for the Buisness Logic Layer
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
    public interface ISoilNeedManager
    {
		 ///<summary>
		 ///Gets Soil Needs List
		 ///@returns: Soil Needs List
		 ///</summary>
		IEnumerable<SoilNeeded> GetSoilNeedsList(string stateLocated, Group group);
		 
		void AddSoilNeed(IEnumerable<SoilNeeded> soilNeeded, User user, Group group);
		
		  ///<summary>
		 ///Edits Soil Need
		 ///@returns: true/false
		 ///</summary>
		bool EditSoilNeed(IEnumerable<SoilNeeded> soilNeeded, User user, Group group);
	}
}