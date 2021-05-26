﻿using MRODBL.BaseClasses;
using System;
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

        Task<string> SelectGUID(int Id);

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
        Task<IEnumerable<dynamic>> GetLocationsList(int nFacilityID);

        /// <summary>
        /// Get Locations for a Facility based on Facility ID and having nFacilityOrgID as null
        /// </summary>
        /// <param name="nFacilityID">Unique Facility ID</param>
        /// <returns>List of Locations for Provided Facility ID</returns>
        Task<IEnumerable<T>> GetLocationsListForOrg(int nFacilityID,int? nFacilityOrgID);
        /// <summary>
        /// Get list of all FacilityID and FacilityName 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetFacilityData();
        /// <summary>
        /// Get list of all OrganizationID and OrganizationName for given facilityID
        /// </summary>
        /// <param name="nFacilityID"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetOrgData(int nFacilityID);
        /// <summary>
        /// /// Get list of all locationID and locationName for given facilityID
        /// </summary>
        /// <param name="nFacilityID"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetLocData(int nFacilityID);
        /// <summary>
        /// /// Get list of all locationID and locationName for given facilityID and OrgID
        /// </summary>
        /// <param name="nFacilityID"></param>
        /// <param name="nFacilityOrgID"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetLocationData(int nFacilityID , int nFacilityOrgID);
        /// <summary>
        /// Performs Inner Join of 2 Tables
        /// </summary>
        /// <param name="cA">Common Column of Table A</param>
        /// <param name="cB">Common Column of Table B</param>
        /// <param name="tA">Table A Name</param>
        /// <param name="tB">Table B Name</param>
        /// <returns>Inner Joined Data of 2 tables</returns>
        Task<IEnumerable<dynamic>> InnerJoin(string cA, string cB, string tA, string tB);

        /// <summary>
        /// Select data where three conditions with AND
        /// </summary>
        /// <param name="PN1">column name 1</param>
        /// <param name="PV1">column value 1</param>
        /// <param name="PN2">column name 2</param>
        /// <param name="PV2">column value 2</param>
        /// <param name="PN3">column name 3</param>
        /// <param name="PV3">column value 3</param>
        /// <returns></returns>
        Task<T> SelectThreeWhereClause(dynamic PN1, dynamic PV1, dynamic PN2, dynamic PV2, dynamic PN3, dynamic PV3);

        /// <summary>
        /// Select data where four conditions with AND
        /// </summary>
        /// <param name="PN1">column name 1</param>
        /// <param name="PV1">column value 1</param>
        /// <param name="PN2">column name 2</param>
        /// <param name="PV2">column value 2</param>
        /// <param name="PN3">column name 3</param>
        /// <param name="PV3">column value 3</param>
        /// <param name="PN4">column name 4</param>
        /// <param name="PV4">column value 4</param>
        /// <returns></returns>
        Task<T> SelectFourWhereClause(dynamic PN1, dynamic PV1, dynamic PN2, dynamic PV2, dynamic PN3, dynamic PV3, dynamic PN4, dynamic PV4);

        /// <summary>
        /// Get Organizations for a Facility based on Facility ID
        /// </summary>
        /// <param name="nFacilityID">Unique Facility ID</param>
        /// <returns>List of Organizations for Provided Facility ID</returns>
        Task<IEnumerable<T>> GetOrganizationsList(int nFacilityID);

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
        /// 
        /// </summary>
        /// <param name="id">ID of record which is to be Deleted</param>
        /// <param name="lnkTable">Name of the link table</param>
        /// <returns></returns>
        bool DeleteOneToMany(int id, string lnkTable);
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
        /// Update requester record in a table
        /// </summary>
        /// <param name="ourModel">Requester record to be updated</param>
        /// <returns>Bool based on whether operation was successful or not</returns>
        bool UpdateSingleRequestor<T>(T ourModel) where T : class;

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
        /// <summary>
        /// To update location with Proper Org ID
        /// </summary>
        /// <param name="nFacilityOrgID">Facility Organization ID</param>
        /// <param name="ids">Ids which needs to be updated.</param>
        /// <returns></returns>
        Task<int> UpdateLocationOrgID(int nFacilityOrgID, int[] ids);
        /// <summary>
        /// To reset location Org ID
        /// </summary>
        /// <param name="ids">Ids which needs to be updated.</param>
        /// <returns></returns>
        Task<int> ResetLocationOrgID(int[] ids);
        /// <summary>
        /// Update requester supporting docs
        /// </summary>
        /// <param name="nRequesterID">Requester Id</param>
        /// <param name="sRelativeFileArray">Array of supporting docs</param>
        /// <param name="sRelativeFileNameArray">Array of supporting docs name</param>
        /// <param name="sWizardName">Wizard name</param>
        /// <returns></returns>
        Task<int> UpdateRequesterSupportDocs(int nRequesterID, string sRelativeFileArray, string sRelativeFileNameArray, string sWizardName);
        /// <summary>
        /// Update requester identity doc
        /// </summary>
        /// <param name="nRequesterID">Requester Id</param>
        /// <param name="sIdentityImage">Base64 identity doc</param>
        /// <param name="sWizardName">Wizard name</param>
        /// <returns></returns>
        Task<int> UpdateRequesterIdentityDoc(int nRequesterID, string sIdentityImage, string sWizardName);
        #endregion

        #region Stored Procedures
        /// <summary>
        /// Get Edit Fields Form Details (SP)
        /// </summary>
        /// <param name="nFacilityID">Facility ID</param>
        /// <param name="nFacilityLocationID">Location ID</param>
        /// <param name="nAdminUserID">Admin user ID</param>
        /// <returns>Facility Field Map Values based on given Facility ID</returns>
        Task<IEnumerable<dynamic>> EditFields(int nFacilityID, int nFacilityLocationID, int nAdminUserID);

        /// <summary>
        /// Get Wizard Config Details (SP)
        /// </summary>
        /// <param name="nFacilityID">Facility ID</param>
        /// <param name="nFacilityLocationID">Location ID</param>
        /// <returns>Wizard Configuration Details</returns>
        Task<object> GetWizardConfigurationAsync(int nFacilityID, int nFacilityLocationID, string sLocationGUID, string sOrgGUID, bool bMultiSelected);

        /// <summary>
        /// Get Logo & Background For Facility based on Facility ID (SP)
        /// </summary>
        /// <param name="nFacilityID">Facility ID</param>
        /// <returns>Logo & Background Details for Facility based on Provided Facility ID</returns>
        Task<dynamic> GetLogoBackGroundforFacilityByGUIDAsync(string sGUID, string sLocationGUID, string sOrgGUID);

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
        /// <summary>
        /// Get Disclaimers Per Facility ID
        /// </summary>
        /// <param name="nFacilityID"> Facility ID</param>
        /// <param name="nFacilityLocationID">Location ID</param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> EditDisclaimers(int nFacilityID, int nFacilityLocationID);
        /// <summary>
        /// Get Audir Report Data
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="sFacilityName"></param>
        /// <param name="sLocationName"></param>
        /// <param name="sAdminName"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetAuditData(string dtStart, string dtEnd, string sFacilityName, string sLocationName, string sAdminName);
        /// <summary>
        /// Get Facility/Location Data
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <param name="sFacilityName"></param>
        /// <param name="sLocationName"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetFacilityLocationData(string dtStart, string dtEnd, string sFacilityName, string sLocationName);
        /// <summary>
        /// Get Facility Configuration Data
        /// </summary>
        /// <param name="sFacilityName"></param>
        /// <param name="sWizardName"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> GetFacilityConfigurationData(string sFacilityName, string sWizardName);
        /// <summary>
        /// Get Record Types And Sensitive Info for generate xml and pdf
        /// </summary>
        /// <param name="nFacilityID">Facility Id</param>
        /// <param name="nFacilityLocationID">Location Id</param>
        /// <returns></returns>
        Task<PDFAndXMLData> GetPDFAndXMLData(int nFacilityID, int nFacilityLocationID);
        #endregion
    }
}
