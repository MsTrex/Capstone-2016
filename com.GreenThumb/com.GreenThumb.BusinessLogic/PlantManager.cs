using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogic
{
    //Created by Stenner Kvindlog 
    public class PlantManager
    {


        ///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchPlantList gets a list of all the plants 
		//calling to the plant accessor
        ///Date: 3/4/16
		///</summary>
        public List<Plant> FetchPlantList(Active active)
        {
            try
            {
                return PlantAccessor.RetrievePlantList(active);
            }
            catch (Exception)
            {
                throw;
            }

        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///fetchPlant gets a plant by plantId
		//calling to the plant accessor
        ///Date: 3/4/16
		///</summary>
        public Plant FetchPlant(int plantId)
        {
            return PlantAccessor.RetrievePlant(plantId);
        }

		
		///<summary>
        ///Author: Stenner Kvindlog         
        ///CreatePlant creates a plant 
		//calling to the plant accessor
        ///Date: 3/4/16
		///</summary>
        public bool CreatePlant(Plant newPlant)
        {
            try
            {
                bool myBool = PlantAccessor.CreatePlant(newPlant);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }
        }

		///<summary>
        ///Author: Stenner Kvindlog         
        ///EditPLant sends new and old plant to database to be edited  
		//calling to the plant accessor
        ///Date: 3/4/16
		///</summary>
        public bool EditPlant(Plant newPlant, Plant oldPlant)
        {
            try
            {
                bool myBool = PlantAccessor.EditPlant(newPlant, oldPlant);
                return myBool;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
