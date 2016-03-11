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
           
            bool result = userRoleMgr.AddNewUserRole(1003, "1002", 1001, DateTime.Now);
      
            //Assert
            Assert.AreEqual(result, true);

        }
        [TestMethod]
        public void TestUserRoleUpdate()
        {
            UserRoleManager userRoleMgr = new UserRoleManager();
            UserRole usrRole = new UserRole();
            UserRole origUsrRole = new UserRole();

            @usrRole.UserID = 1004;
            @usrRole.RoleID = "1001";
            @usrRole.CreatedBy = 1002;
            @usrRole.CreatedDate = DateTime.Now;

            
           
        //    usr.RegionID = Convert.DBNull;
           
            //Act

            bool result = userRoleMgr.ChangeUserRole(usrRole);

            //Assert
              Assert.AreEqual(result, true);
        }
        [TestMethod]
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
        }
    }
}
