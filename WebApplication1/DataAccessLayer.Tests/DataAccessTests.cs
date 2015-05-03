using System;
using System.Linq;
using Data.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void TestDatabaseConnection()
        {
            BiryukovTestDbContext biryukovTestDbContext = new BiryukovTestDbContext();

            var query = from employees in biryukovTestDbContext.Employees
                     select employees;

            Assert.IsNotNull(query);
        }
    }
}
