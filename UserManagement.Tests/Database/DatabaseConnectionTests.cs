using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data;

namespace UserManagement.Tests
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        [TestMethod]
        public void TestDatabaseConnection()
        {
            // Arrange
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    // Act
                    context.Database.Connection.Open();

                    // Assert
                    Assert.IsTrue(context.Database.Connection.State == ConnectionState.Open);
                }
                finally
                {
                    // Ensure that the connection is closed
                    context.Database.Connection.Close();
                }
            }
        }
    }

}
