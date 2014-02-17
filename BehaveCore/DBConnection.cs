using System;
using System.Data.SqlClient;

namespace Behave.BehaveCore
{
    public static class DBConnection
    {
        private const string AZURE_MANDATED_PREFIX = "SQLCONNSTR_";
        private const string AZURE_DB_STRING_NAME = AZURE_MANDATED_PREFIX + "BEHAVE_DB_STRING";
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
