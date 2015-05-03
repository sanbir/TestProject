using System;
using System.Linq;
using ClassLibrary1;
using Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Model1 biryukovTestDbContext = new Model1();

            var aa = from a in biryukovTestDbContext.Employees
                     select a;
            Assert.IsNotNull(aa);
        }
    }
}
