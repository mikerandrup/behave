using System;
using System.Data.SqlClient;
using Behave.BehaveCore.DBUtils;
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
                using (SqlConnection conn = Connection.Create())
                {
                    conn.Open();
                    Assert.IsTrue(true);
                }
            }
            catch (Exception exc) // to inspect exception
            {
                Assert.IsTrue(false);
            }
        }
    }
}
