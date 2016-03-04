///<summary>
///Author: Chris Schwebach
///Equipment Needs Interface for the Buisness Logic Layer
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
    public interface IEquipmentNeedManager
    {
		///<summary>
		///Gets Equipment Needs list
		///@returns: Equipment Needs List
		///</summary>
		IEnumerable<EquipmentNeeded> GetEquipmentNeededList(string stateLocated, Group group);
		  
		void AddEquipmentNeed(IEnumerable<EquipmentNeeded> equipmentNeeded, User user, Group group);
		
		///<summary>
		///Edits Equipment Needs
		///returns: true/false
		///</summary>
		bool EditEquipmentNeed(IEnumerable<EquipmentNeeded> equipmentNeeded, User user, Group group);
	}
}
		 