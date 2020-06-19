using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using MRODBL.BaseClasses;

namespace MRODBL.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        protected string sConnect = string.Empty;
        //protected DBConnectionInfo DBInfo;
        protected string sTableName;
        protected string sKeyName;
        protected void Init(string sConnectIn, string TableNameIn, string sKeyNameIn)
        {
            sKeyName = sKeyNameIn;
            sTableName = TableNameIn;
            sConnect = sConnectIn;
        }
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }
        public virtual int? Insert(T item)
        {
            int? Id = null;
            using (IDbConnection cn = new SqlConnection(sConnect))
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                using (var trans = cn.BeginTransaction())
                {
                    T returnedItem = cn.Insert<T>(sTableName, parameters, sKeyName, trans);
                    object obj = returnedItem.GetType().GetProperty(sKeyName).GetValue(returnedItem, null);
                    //int nId;
                    if (!Int32.TryParse(obj + "", out int nId))
                        return null;
                    Id = nId;
                }
            }
            return Id;
        }
        public bool Delete(int ID)
        {
            int rowsAffected = 0;
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    rowsAffected = db.Execute(
                                    "DELETE " + sTableName + @" 
                                    WHERE " + sKeyName + @" =@ID",
                                    new { ID = ID }, trans);
                    trans.Commit();
                }
            }
            return rowsAffected > 0;
        }


        public async Task<IEnumerable<T>> GetAllASC(int rows, string sort)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                if (string.IsNullOrEmpty(sort))
                    sort = sKeyName;
                string SqlString = "SELECT TOP " + rows + @" *
                                    FROM " + sTableName + @"
                                    Order by " + sort + " ASC";
                return await db.QueryAsync<T>(SqlString);
            }
        }
        public async Task<IEnumerable<T>> GetAllDESC(int rows, string sort)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                if (string.IsNullOrEmpty(sort))
                    sort = sKeyName;
                string SqlString = "SELECT TOP " + rows + @" *
                                    FROM " + sTableName + @"
                                    Order by " + sort + " DESC";
                return await db.QueryAsync<T>(SqlString);
            }
        }


        public async Task<T> Select(int Id)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                return await db.QueryFirstAsync<T>(@"SELECT *
                                    FROM " + sTableName + @" 
                                    WHERE " + sKeyName + @" =@ID",
                                    new { ID = Id });
            }
        }

        public bool Update(T ourModel)
        {
            int rowsAffected = 0;
            using (IDbConnection cn = new SqlConnection(sConnect))
            {
                var parameters = (object)Mapping(ourModel);
                cn.Open();
                using (var trans = cn.BeginTransaction())
                {
                    rowsAffected = cn.Update(sTableName, parameters, sKeyName, trans);
                }
            }
            return rowsAffected > 0;
        }

        public async Task<bool> InsertMany(List<T> ourModels)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                int count = 0;
                using (var trans = db.BeginTransaction())
                {
                    count = await db.InsertAsync(ourModels, trans);
                    trans.Commit();
                }
                if (count > 0) { return true; }
                else { return false; }
            }
        }

        public async Task<int> GetLatestId()
        {
            string sql = "SELECT IDENT_CURRENT('" + sTableName + "')";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                int ID = await db.ExecuteScalarAsync<int>(sql);
                return ID;
            }
        }

        public async Task<bool> ToggleSoftDelete(string sColName, int nID)
        {
            string sql = "SELECT " + sColName + " FROM " + sTableName + " WHERE " + sKeyName + " = " + nID;
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                int nToggle = db.ExecuteScalar<int>(sql);
                nToggle = nToggle == 0 ? 1 : 0;
                string sdSql = "UPDATE " + sTableName + " SET " + sColName + " = " + nToggle + " WHERE " + sKeyName + " = " + nID;
                int nRowAffected = await db.ExecuteAsync(sdSql);
                return nRowAffected == 1 ? true : false;
            }
        }

        public async Task<IEnumerable<dynamic>> InnerJoin(string cA, string cB, string tA, string tB)
        {
            string SqlString = "SELECT * FROM " + tA + " INNER JOIN " + tB + " ON " + tA + "." + cA + " = " + tB + "." + cB;
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                return await db.QueryAsync(SqlString);
            }
        }
        public async Task<bool> UpdateMany(List<T> ourModels)

        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                bool continueAhead = false;

                continueAhead = await db.UpdateAsync(ourModels);

                return continueAhead;
            }
        }

        public async Task<IEnumerable<T>> SelectWhere(dynamic paramKeyName, dynamic paramValue)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString = "SELECT * FROM " + sTableName + " WHERE " + paramKeyName + " = @ID";
                return await db.QueryAsync<T>(SqlString, new { ID = paramValue });
            }
        }
        public async Task<int> CountWhere(dynamic paramKeyName, dynamic paramValue)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString = "SELECT COUNT(nFacilityID) FROM " + sTableName + " WHERE " + paramKeyName + " = @ID";
                return await db.ExecuteScalarAsync<int>(SqlString, new { ID = paramValue });
            }
        }

        public async Task<IEnumerable<dynamic>> EdiftFields(int ID)
        {
            string SqlString = "spGetPatientFormBynfacilityID";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                IEnumerable<dynamic> a=  await db.QueryAsync(SqlString, new { @ID = ID },commandType: CommandType.StoredProcedure);
                return a;
            }
        }
        //public async Task<int> GetlatestROIID()
        //{
        //    using (SqlConnection db = new SqlConnection(sConnect))
        //    {
        //        string SqlString = "SELECT  FROM " + sTableName + " WHERE " + paramKeyName + " = @ID";
        //        return await db.ExecuteScalarAsync<int>(SqlString, new { ID = paramValue });
        //    }
        //}
        public int GetROILocationID(dynamic paramKeyName, dynamic paramValue)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString = "SELECT TOP(1) nROILocationID FROM " +sTableName+ " WHERE "+paramKeyName+" ="+paramValue+" ORDER BY nROILocationID DESC";
                return  db.QueryFirstOrDefault<int>(SqlString, new { ID = paramValue });
            }
        }
    }
}
