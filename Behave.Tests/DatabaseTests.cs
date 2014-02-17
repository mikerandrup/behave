using System;
using System.Data.SqlClient;
using Behave.BehaveCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Behave.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CanConnect()
        {
            try
            {
                SqlConnection conn = DBConnection.Create();
                conn.Open();
                Assert.IsTrue(true);
            }
            catch (Exception exc) // done this way to inspect exception
            {
                Assert.IsTrue(false);
            }
        }
    }
}
