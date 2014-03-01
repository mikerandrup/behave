using System.Data.SqlClient;
using Behave.BehaveCore.DBUtils;

namespace Behave.BehaveCore.DataClasses
{
    public interface ICrudOrmItem
    {
        DbResult LoadFromDB(SqlConnection dbConn);
        DbResult SaveToDB(SqlConnection dbConn);
        DbResult DeleteFromDB(SqlConnection dbConn);
    }

    public interface ICrudOrmList
    {
        DbResult LoadFromDB(SqlConnection dbConn);
    }
}
