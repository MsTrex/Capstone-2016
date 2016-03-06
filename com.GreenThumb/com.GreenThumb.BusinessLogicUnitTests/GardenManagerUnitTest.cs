using com.GreenThumb.BusinessLogic.Interfaces;
using com.GreenThumb.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    /// <summary>
    /// Tests to 
    /// </summary>
    [TestClass]
    public class GardenManagerUnitTest
    {
        IGardenManager gardenManager = null;
        Garden garden = null;
        [TestMethod]
        public void GetGroupListReturnGroupList()
        {
            garden = new Garden()
            {
                // ToDO : Add Garden parameters
                //GroupID = 1000,
                //UserID = 1001,
                //GardenDescription = "This is a Test Garden",
                //GardenRegion = "Test Garden region"

            };
            ///create a new instance of list of group-(typeOf(List<Group> group)
            ///arrange/act
            bool isSuccess = gardenManager.CreateGarden(garden);
            Assert.AreEqual(true, isSuccess);
            ///assert, that it's not null-populated with actual date, so put more then one assert statement
            ///List<Group> group = groupManager.Group;
            ///assert
            ///Assert.AreNotEqual(0, group.Count);

        }
    }
}
