using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class RoleManagerUnitTests
    {
        /// <summary>
        /// Steve Hoover
        /// 4/28/16
        /// Tests to see if List<Role> is returned
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var roleList = RoleAccessor.RetrieveRoleList();

            Assert.IsNotNull(roleList);
        }
    }
}
