using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogic
{
    public class RecipeManager
    {
        ///<summary>
        ///Author: Chris Schwebach
        ///Recipe logic for user parameter input to insert a recipe  
        ///Date: 3/19/16 
        ///</summary>
        public bool AddNewRecipe(string title, string category, string directions, int userId)
        {
            bool result = true;

            var newRecipe = new Recipe()
            {
                Title = title,
                Category = category,
                Directions = directions
            };

            if (title.Length < 1 || title.Length > 50)
            {
                throw new ApplicationException("Title for the recipe is required! Must Be less than 50 characters in length");
            }
            else if (category.Length < 1)
            {
                throw new ApplicationException("Must choose a category!");
            }
            else if (directions.Length < 1)
            {
                throw new ApplicationException("Ingredients and directions are required!");
            }

            try
            {
                if (RecipeAccessor.InputRecipe(newRecipe, userId) == 1)
                {
                    result = true;
                }
                else {
                    result = false;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Invalid Input!");
            }

            return result;
        }
    }
}
