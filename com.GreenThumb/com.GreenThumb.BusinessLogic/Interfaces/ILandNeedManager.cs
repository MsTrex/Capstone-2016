///<summary>
///Author: Chris Schwebach
///Land Needs Interface for the Buisness Logic Layer
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
    public interface ILandNeedManager
    {
		///<summary>
		///Gets Land Needs List
		///@returns: Land Needs List
		///</summary>
		IEnumerable<LandNeeded> GetLandNeedList(string city, Group group);
		 
		void AddLandNeed(IEnumerable<LandNeeded> landNeeded, User user, Group group);
		
		///<summary>
		///Edits Land Needs
		///@returns: true/false
		///</summary>
		bool EditLandNeed(IEnumerable<LandNeeded> landNeeded, User user, Group group);
	}
}