///<summary>
///Author: Chris Schwebach
///Equipment Donations Interface for the Buisness Logic Layer
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
    public interface IEquipmentDonationManager
    {
		///<summary>
		///Gets equipment donations list
		///@returns: Equipment Donations List
		///</summary>
		IEnumerable<EquipmentDonated> GetEquipmentDonationList(string stateLocated, User user);
		  
		void AddNewEquipmentDonation(IEnumerable<EquipmentDonated> equipmentDonated, User user);
		
		///<summary>
		///Edits Equipment Donation
		///returns: true/false
		///</summary>
		bool EditEquipmentDonation(IEnumerable<EquipmentDonated> equipmentDonated, User user);
		
	}
}