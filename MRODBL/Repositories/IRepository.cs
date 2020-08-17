using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRODBL.Repositories
{
    internal interface IRepository<T>
    {
        #region SELECT Queries
        ///<summary>
        /// Get All Records of Table Ascending
        /// </summary>
        /// <param name="amount">Number of Rows to SELECT</param>
        /// <param name="sort">Provide Sort key of Table, if not provided Primary Key will be Used</param>
        /// <returns>List of Records Ascending Order</returns>
        Task<IEnumerable<T>> GetAllASC(int amount, string sort);

        ///<summary>
        /// Get All Records of Table Descending
        /// </summary>
        /// <param name="amount">Number of Rows to SELECT</param>
        /// <param name="sort">Provide Sort key of Table, if not provided Primary Key will be Used</param>
        /// <returns>List of Records Descending Order</returns>
        Task<IEnumerable<T>> GetAllDESC(int amount, string sort);

        ///<summary>
        /// Get Single Record of a Table
        /// </summary>
        /// <param name="Id">Unique Id for Searching Record within Table</param>
        /// <returns>Single Record of Table</returns>
        Task<T> Select(int Id);

        /// <summary>
        /// Returns Latest Id of a Table
        /// </summary>
        /// <returns>Latest Id for a Table</returns>
        Task<int> GetLatestId();

        /// <summary>
        /// Simple SELECT Based on Single Condition
        /// </summary>
        /// <param name="paramKeyName">Condition Column Name</param>
        /// <param name="paramValue">Condition Column Value</param>
        /// <returns>List of Matching Records/Single Record</returns>
        Task<IEnumerable<T>> SelectWhere(dynamic paramKeyName, dynamic paramValue);
        /// <summary>
        /// Select Location By LocationName for validation
        /// </summary>
        /// <param name="nFacilityLocationID"></param>
        /// <param name="sFacilityLocationName"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> SelectLocationByLocationName(int nFacilityLocationID, string sFacilityLocationName);

        /// <summary>
        /// Simple GetLocationByNormalizedName Based on Single Condition
        /// </summary>
        /// <param name="nFacilityID">Condition Facility Id</param>
        /// <param name="sNormalizedLocationName">Condition sNormalizedLocationName</param>
        /// <returns>List of Matching Records/Single Record</returns>
        Task<IEnumerable<dynamic>> GetLocationByNormalizedName(int nFacilityID, int nFacilityLocationID, string sNormalizedLocationName);

        /// <summary>
        /// Count of Rows Matching given Condition
        /// </summary>
        /// <param name="paramKeyName">Condition Column Name</param>
        /// <param name="paramValue">Condition Column Value</param>
        /// <returns>Count of Matching Rows</returns>
        Task<int> CountWhere(dynamic paramKeyName, dynamic paramValue);

        /// <summary>
        /// Get Latest ROI ID for a Location (Populating on Add Location Screen)
        /// Column Name: 'nFacilityID'
        /// </summary>
        /// <param name="paramKeyName">Column Name</param>
        /// <param name="paramValue">Column Name Value</param>
        /// <returns></returns>
        //int GetROILocationID(dynamic paramKeyName, dynamic paramValue);

        /// <summary>
        /// Get Locations for a Facility based on Facility ID
        /// </summary>
        /// <param name="nFacilityID">Unique Facility ID</param>
        /// <returns>List of Locations for Provided Facility ID</returns>
        Task<IEnumerable<T>> GetLocationsList(int nFacilityID);

        /// <summary>
        /// Performs Inner Join of 2 Tables
        /// </summary>
        /// <param name="cA">Common Column of Table A</param>
        /// <param name="cB">Common Column of Table B</param>
        /// <param name="tA">Table A Name</param>
        /// <param name="tB">Table B Name</param>
        /// <returns>Inner Joined Data of 2 tables</returns>
        Task<IEnumerable<dynamic>> InnerJoin(string cA, string cB, string tA, string tB);
        #endregion

        #region INSERT Queries
        /// <summary>
        /// Insert Single Record in a Table
        /// </summary>
        /// <param name="ourModel">Record to be Inserted</param>
        /// <returns>Id of newly generated Record</returns>
        int? Insert(T ourModel);

        ///<summary>
        /// Insert Many Records in a Table
        /// </summary>
        /// <param name="models">List of Records</param>
        /// <returns>Bool based on whether operations where successful or not</returns>
        Task<bool> InsertMany(List<T> models);
        #endregion

        #region Toggle/Delete Queries
        ///<summary>
        /// Soft Delete a Record in a Table
        /// </summary>
        /// <param name="sdColName">Boolean Column Name</param>
        /// <param name="ID">ID of record which is to be Soft Deleted</param>
        /// <returns>Bool based on whether operation was successful or not</returns>
        Task<bool> ToggleSoftDelete(string sdColName, int ID);

        /// <summary>
        /// Delete a Record in a Table
        /// </summary>
        /// <param name="nID">ID of Record which is to be deleted</param>
        /// <returns>Bool based on whether operation was successful or not</returns>
        bool Delete(int nID);
        #endregion

        #region Update Queries
        /// <summary>
        /// Update a Record in a Table
        /// </summary>
        /// <param name="ourModel">Record to be Updated</param>
        /// <returns>Bool based on whether operation was successful or not</returns>
        bool Update(T ourModel);

        /// <summary>
        /// Update Records in a Table
        /// </summary>
        /// <param name="ourModels">List of Records to be Updated</param>
        /// <returns>Bool based on whether operation was successful or not</returns>
        bool UpdateMany(List<T> ourModels);
        /// <summary>
        /// To update requester object after request submitted
        /// </summary>
        /// <param name="nRequesterID">Requester Id</param>
        /// <param name="bRequestAnotherRecord">True if requester selected Request Another Record</param>
        /// <param name="nFeedbackRating">1 to 5 rating</param>
        /// <param name="sFeedbackComment">Comments of requester</param>
        /// <param name="sWizardName">Name of the wizard</param>
        /// <returns></returns>
        Task<int> UpdateRequesterFeedback(int nRequesterID, bool bRequestAnotherRecord, int nFeedbackRating, string sFeedbackComment,string sWizardName);
        #endregion

        #region Stored Procedures
        /// <summary>
        /// Get Edit Fields Form Details (SP)
        /// </summary>
        /// <param name="ID">Facility ID</param>
        /// <returns>Facility Field Map Values based on given Facility ID</returns>
        Task<IEnumerable<dynamic>> EditFields(int ID);

        /// <summary>
        /// Get Wizard Config Details (SP)
        /// </summary>
        /// <param name="nFacilityID">Facility ID</param>
        /// <param name="nFacilityLocationID">Location ID</param>
        /// <returns>Wizard Configuration Details</returns>
        Task<object> GetWizardConfigurationAsync(int nFacilityID, int nFacilityLocationID);

        /// <summary>
        /// Get Logo & Background For Facility based on Facility ID (SP)
        /// </summary>
        /// <param name="nFacilityID">Facility ID</param>
        /// <returns>Logo & Background Details for Facility based on Provided Facility ID</returns>
        Task<dynamic> GetLogoBackGroundforFacilityByGUIDAsync(string sGUID);

        /// <summary>
        /// Get Logo & Background For Location based on Location ID (SP)
        /// </summary>
        /// <param name="nLocationID">Location ID</param>
        /// <returns>Logo & Background Details</returns>
        Task<dynamic> GetLogoBackGroundforLocationsync(int nLocationID);

        /// <summary>
        /// Insert Dependency Records for Facility (SP)
        /// </summary>
        /// <param name="nFacilityID">Unique Facility Id</param>
        /// <param name="nConnectionID">Connection ID </param>
        /// <param name="nAdminUserId">Logged In Admin User ID</param>
        void AddDependencyRecordsForFacility(int nFacilityID, int nConnectionID, int nAdminUserId);
        /// <summary>
        /// Add Dependency Records for Facility Location
        /// </summary>
        /// <param name="nFacilityLocationID">Location ID</param>
        /// <param name="nFacilityID">Facility ID</param>
        void AddDependencyRecordsForFacilityLocation(int nFacilityLocationID, int nFacilityID);
        /// <summary>
        /// Get Admin User ID
        /// </summary>
        /// <param name="nAdminUserID">Admin ID</param>
        /// <param name="sUPN">Admin UPN</param>
        /// <param name="sEmail">Admin Email</param>
        /// <returns>Admin User ID (int)</returns>
        Task<int> GetAdminUserID(string sName, string sUPN, string sEmail);
        #endregion
    }
}
