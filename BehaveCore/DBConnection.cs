using System;
using System.Data.SqlClient;

namespace Behave.BehaveCore
{
    public static class DBConnection
    {
        private const string AZURE_DB_STRING_NAME = "behave_azure_db_string";
        private static string AzureConnectionString
        {
            get {
                string connectionStringValue = Environment.GetEnvironmentVariable(AZURE_DB_STRING_NAME);

                if (String.IsNullOrWhiteSpace(connectionStringValue))
                {
                    connectionStringValue = String.Empty;
                }

                return connectionStringValue;
            }
        }

        public static SqlConnection Create()
        {
            var connectionString = AzureConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
