using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.DataAccess;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.BusinessLogic;
using System.Collections;
using com.GreenThumb.BussinessLogic;

namespace com.GreenThumb.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUserRoleInsert()
        {

            //Arrange
            UserRoleManager userRoleMgr = new UserRoleManager();
            UserRole usrRole = new UserRole();
            
            
            //Act

            bool result = userRoleMgr.AddNewUserRole(1003, "Admin");
      
            //Assert
            Assert.AreEqual(result, true);
           

        }
        [TestMethod]
        public void TestUserRoleUpdate()
        {
            UserRoleManager userRoleMgr = new UserRoleManager();
            UserRole usrRole = new UserRole();
            UserRole origUsrRole = new UserRole();

            int testUserID = 1004;
            string testRoleID = "1001";
            bool testActive = false;
           
        //    usr.RegionID = Convert.DBNull;
           
            //Act

            bool result = userRoleMgr.ChangeUserRoleStatus(testUserID, testRoleID, testActive);

            //Assert
              Assert.AreEqual(result, false);
        }
  /*      [TestMethod]
        public void TestUserRoleRemove()
        {
            UserRoleManager userRoleMgr = new UserRoleManager();
            UserRole usrRole = new UserRole();

            @usrRole.UserID = 1004;
            @usrRole.RoleID = "1001";
  
            //Act

            bool result = userRoleMgr.DeleteUserRole(@usrRole.UserID, @usrRole.RoleID);

            //Assert
            Assert.AreEqual(result, true);
        } */
    }
}
