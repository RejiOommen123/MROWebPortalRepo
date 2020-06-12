using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRODBL.Repositories
{
    internal interface IRepository<T>
    {
        #region List of Methods

        #endregion
        ///<summary
        /// Get All Records of a Table
        /// </summary>
        /// <param name="rows">Number of Rows to Return</param>
        /// <param name="sort">Primary key of Table</param>
        Task<IEnumerable<T>> GetAll(int amount, string sort);

        ///<summary
        /// Get Single Record of a Table
        /// </summary>
        /// <param name="Id">Unique Id for Searching</param>
        Task<T> Select(int Id);

        ///<summary
        /// Insert Many Records 
        /// </summary>
        /// <param name="models">List of Records</param>
        Task<bool> InsertMany(List<T> models);
        Task<int> GetLatestId();

        ///<summary
        /// Soft Delete Functionality
        /// </summary>
        /// <param name="sdColName">Boolean Column Name</param>
        /// <param name="ID">Primary key of Table</param>
        Task<bool> ToggleSoftDelete(string sdColName,int ID);

        ///<summary
        /// Insert Single Record in a Table
        /// </summary>
        /// <param name="ourModel">Table Data - Generic Type</param>
        int? Insert(T ourModel);

        bool Delete(int nID);

        bool Update(T ourModel);

        Task<IEnumerable<dynamic>> InnerJoin(string cA, string cB, string tA, string tB);
        Task<bool> UpdateMany(List<T> ourModels);
        Task<IEnumerable<T>> SelectWhere(dynamic paramKeyName, dynamic paramValue);
        Task<int> CountWhere(dynamic paramKeyName, dynamic paramValue);
        //Task<int> GetLatestROIID();
    }
}
