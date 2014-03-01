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

            #pragma warning disable 0168
            catch (SqlException exc)
            #pragma warning restore 0168
            {
                Assert.IsTrue(false);
            }
        }
    }
}
