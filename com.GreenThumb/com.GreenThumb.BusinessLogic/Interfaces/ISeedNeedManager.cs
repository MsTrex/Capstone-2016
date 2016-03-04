///<summary>
///Author: Chris Schwebach
///Seed Needs Interface for the Buisness Logic Layer
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
    public interface ISeedNeedManager
    {
		///<summary>
		///Gets Seed Needs List
		///@returns: Seed Needs List
		///</summary>
		IEnumerable<SeedNeeded> GetSeedNeedList(string stateLocated, Group group);
		 
		void AddSeedNeed(IEnumerable<SeedNeeded> seedNeeded, User user, Group group);
		
		///<summary>
		///Edits Seed Need
		///@returns: true/false
		///</summary>
		bool EditSeedNeed(IEnumerable<SeedNeeded> seedNeeded, User user, Group group);

	}
}