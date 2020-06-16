using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MRODBL.BaseClasses
{
    public static class DapperExtensions
    {
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param, string sIDFieldName, IDbTransaction trans)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param, sIDFieldName), param, trans);
            trans.Commit();
            return result.First();
        }

        public static int Update(this IDbConnection cnn, string tableName, dynamic param, string sKeyName, IDbTransaction trans)
        {
            //UpdateHere
            int a = SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param, sKeyName), param, trans);
            trans.Commit();
            return a;
        }
    }
}
