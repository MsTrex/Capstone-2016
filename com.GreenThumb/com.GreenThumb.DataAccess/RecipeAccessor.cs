using com.GreenThumb.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace com.GreenThumb.DataAccess
{
   public class RecipeAccessor
    {
        ///<summary>
        ///Author: Chris Schwebach
        ///DB accessor to insert a recipe  
        ///Date: 3/19/16
        ///</summary>
       public static int InputRecipe(Recipe recipe, int UserId)
       {
           int rowCount = 0;

           DateTime dateSubmited = DateTime.Now;
           
           // get a connection
           var conn = DBConnection.GetDBConnection();

           // we need a command object (the command text is in the stored procedure)
           var cmd = new SqlCommand("Expert.spInsertRecipes", conn);

           // set the command type for stored procedure
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("@Title", recipe.Title);
           cmd.Parameters.AddWithValue("@Category", recipe.Category);
           cmd.Parameters.AddWithValue("@Directions", recipe.Directions);
           cmd.Parameters.AddWithValue("@CreatedBy", UserId);
           cmd.Parameters.AddWithValue("@CreatedDate", dateSubmited);
           cmd.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
           cmd.Parameters.AddWithValue("@ModifiedDate", DBNull.Value);

           cmd.Parameters.Add(new SqlParameter("RowCount", SqlDbType.Int));
           cmd.Parameters["RowCount"].Direction = ParameterDirection.ReturnValue;

           try
           {
               conn.Open();
               rowCount = (int)cmd.ExecuteNonQuery();
           }
           catch (Exception)
           {
               throw new ApplicationException("Invalid Selection!");
           }
           finally
           {
               conn.Close();
           }

           return rowCount;
       }
        
    }
}
