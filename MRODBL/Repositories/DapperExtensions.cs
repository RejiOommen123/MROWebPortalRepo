using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MRODBL.BaseClasses
{
    public static class DapperExtensions
    {
        #region Extension Methods Dapper
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param, string sIDFieldName, IDbTransaction transaction)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param, sIDFieldName), param, transaction);
            transaction.Commit();
            return result.First();
        }

        public static int Update(this IDbConnection cnn, string tableName, dynamic param, string sKeyName, IDbTransaction transaction)
        {
            int count = SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param, sKeyName), param, transaction);
            transaction.Commit();
            return count;
        }
        #endregion
    }
}
