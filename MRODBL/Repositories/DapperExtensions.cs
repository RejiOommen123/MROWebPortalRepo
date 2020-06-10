using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRODBL.BaseClasses
{
    public static class DapperExtensions
        {
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param, string sIDFieldName)
            {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param, sIDFieldName), param);
            return result.First();
            }

        public static int Update(this IDbConnection cnn, string tableName, dynamic param, string sKeyName)
            {
            //UpdateHere
             int a = SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param, sKeyName), param);
            return a;
        }
        }
    }
