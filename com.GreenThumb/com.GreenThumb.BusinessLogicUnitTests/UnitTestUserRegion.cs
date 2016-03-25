using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObject;
using com.GreenThumb.BusinessObjects;

namespace com.GreenThumb.UnitTest
{
    [TestClass]
    public class UnitTestUserRegion
    {
        [TestMethod]
        public void UpdateMethod()
        {
            //Arrange
            UserRegionManager userMgr = new UserRegionManager();
            User usr = new User();

            //Act
            bool result = userMgr.ChangeUserData(1007, 1);

            //Assert
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void SelectMethod()
        {
            //Arrange
            UserRegionManager userMgr = new UserRegionManager();
            User usr = new User();

            //Act
            var userNo = 1001;
            User result = userMgr.DisplayUserRecord(userNo);

            //Assert
            Assert.AreEqual(result.UserID, userNo);
        }
    }
}
