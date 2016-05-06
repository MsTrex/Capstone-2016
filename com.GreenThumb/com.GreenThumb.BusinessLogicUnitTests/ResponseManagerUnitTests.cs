using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.GreenThumb.BusinessLogic;
using com.GreenThumb.BusinessObjects;
using com.GreenThumb.DataAccess;

namespace com.GreenThumb.BusinessLogicUnitTests
{
    [TestClass]
    public class ResponseManagerUnitTests
    {
        ResponseManager responseManager = new ResponseManager();
        Response response = null;
        /// <summary>
        /// Steve Hoover
        /// 4/28/16
        /// Test to verify response creation method is valid
        /// </summary>
        [TestMethod]
        public void TestCreateNewResponse()
        {
   //         response = new Response(1001,DateTime.Now,"this is a response",1000);
            bool test = false;

            test = QuestionResponseAccessor.CreateResponse(response);

            Assert.AreEqual(test, false);

        }
    }
}
