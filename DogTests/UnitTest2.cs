using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DogDirectory.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace DogTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void BreedListReturnType()
        {
            DogsController controller = new DogsController();
            var actual = controller.Index();
            Assert.IsInstanceOfType(actual, typeof(Task<ActionResult>));

        }

        [TestMethod]
        public void DetailsReturnType()
        {
            DogsController controller = new DogsController();
            var actual = controller.Details("hound");
            Assert.IsInstanceOfType(actual, typeof(Task<ActionResult>));

        }

        [TestMethod]
        public void MissingReturnType()
        {
            DogsController controller = new DogsController();
            var actual = controller.Missing();
            Assert.IsInstanceOfType(actual, typeof(ActionResult));

        }


    }
}
