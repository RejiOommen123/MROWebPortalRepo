using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRODBL.Repositories
{
    internal interface IRepository<T>
    {

        ///<summary>
        /// Get All Records of Table ASC
        /// </summary>
        /// <param name="amount">Number of Rows to SELECT</param>
        /// <param name="sort">Provided Sort key of Table, if not provided Primary Key will be Used</param>
        /// <returns>List of Records</returns>
        Task<IEnumerable<T>> GetAllASC(int amount, string sort);

        ///<summary>
        /// Get All Records of Table DESC
        /// </summary>
        /// <param name="amount">Number of Rows to SELECT</param>
        /// <param name="sort">Provided Sort key of Table, if not provided Primary Key will be Used</param>
        /// <returns>List of Records</returns>
        Task<IEnumerable<T>> GetAllDESC(int amount, string sort);

        ///<summary>
        /// Get Single Record Table
        /// </summary>
        /// <param name="Id">Unique Id for Searching Record within Table</param>
        /// <returns>Single Record</returns>
        Task<T> Select(int Id);

        ///<summary>
        /// Insert Many Records Table
        /// </summary>
        /// <param name="models">List of Records</param>
        /// <returns>Bool based whether operations where success or not</returns>
        Task<bool> InsertMany(List<T> models);

        /// <summary>
        /// Returns Latest Id of a Table
        /// </summary>
        /// <returns>Latest Id for a Table</returns>
        Task<int> GetLatestId();

        ///<summary>
        /// Soft Delete a Record - Table
        /// </summary>
        /// <param name="sdColName">Boolean Column Name</param>
        /// <param name="ID">ID of record which is to be Soft Deleted</param>
        /// <returns>Bool based whether operation was success or not</returns>
        Task<bool> ToggleSoftDelete(string sdColName,int ID);

        /// <summary>
        /// Insert Single Record - Table
        /// </summary>
        /// <param name="ourModel">Table Data - Generic Type</param>
        /// <returns>Id of newly generated Record</returns>
        int? Insert(T ourModel);

        /// <summary>
        /// Delete a Record - Table
        /// </summary>
        /// <param name="nID">ID of Record which is to be deleted</param>
        /// <returns>Bool based on whether operation was success or not</returns>
        bool Delete(int nID);

        /// <summary>
        /// Update a Record - Table
        /// </summary>
        /// <param name="ourModel">Record to be Updated</param>
        /// <returns>Bool bas on sucees of update operation</returns>
        bool Update(T ourModel);

        /// <summary>
        /// Performs Inner Join of 2 Tables
        /// </summary>
        /// <param name="cA">Common Column of Table A</param>
        /// <param name="cB">Common Column of Table B</param>
        /// <param name="tA">Table A Name</param>
        /// <param name="tB">Table B Name</param>
        /// <returns>Dynamic of 2 Tables</returns>
        Task<IEnumerable<dynamic>> InnerJoin(string cA, string cB, string tA, string tB);

        /// <summary>
        /// Update Records - Table
        /// </summary>
        /// <param name="ourModels">List of Records to be Updated</param>
        /// <returns>Bool based on whether operation success or not</returns>
        Task<bool> UpdateMany(List<T> ourModels);

        /// <summary>
        /// Simple Select Based on Single Condition
        /// </summary>
        /// <param name="paramKeyName">Condition Column Name</param>
        /// <param name="paramValue">Condition Column Value</param>
        /// <returns>List of Matching Records</returns>
        Task<IEnumerable<T>> SelectWhere(dynamic paramKeyName, dynamic paramValue);

        /// <summary>
        /// Count of Rows Matching given Condition
        /// </summary>
        /// <param name="paramKeyName">Condition Column Name</param>
        /// <param name="paramValue">Condition Column Value</param>
        /// <returns>Count of Matching Rows</returns>
        Task<int> CountWhere(dynamic paramKeyName, dynamic paramValue);

        /// <summary>
        /// Get Edit Fields Form
        /// </summary>
        /// <param name="ID">Facility ID</param>
        /// <returns>IEnumerable<dynamic<</returns>
        Task<IEnumerable<dynamic>> EdiftFields(int ID);
    }
}
