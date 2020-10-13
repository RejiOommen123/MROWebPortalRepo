﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using MRODBL.BaseClasses;

namespace MRODBL.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        #region Common Fields & Helper Method
        protected string sConnect = string.Empty;
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
        internal virtual Requesters Mapping(Requesters item)
        {
            return item;
        }
        #endregion

        #region SELECT Queries 
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
        //public int GetROILocationID(dynamic paramKeyName, dynamic paramValue)
        //{
        //    using (SqlConnection db = new SqlConnection(sConnect))
        //    {
        //        string SqlString = "SELECT TOP(1) nROILocationID FROM " + sTableName + " WHERE " + paramKeyName + " =" + paramValue + " ORDER BY nROILocationID DESC";
        //        return db.QueryFirstOrDefault<int>(SqlString, new { ID = paramValue });
        //    }
        //}
        public async Task<IEnumerable<T>> SelectWhere(dynamic paramKeyName, dynamic paramValue)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString = "SELECT * FROM " + sTableName + " WHERE " + paramKeyName + " = @ID";
                return await db.QueryAsync<T>(SqlString, new { ID = paramValue });
            }
        }
        public async Task<IEnumerable<T>> SelectLocationByLocationName( int nFacilityLocationID, string sLocationName)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString =
                   "SELECT * FROM " +
                     "tblFacilityLocations " +
                     "INNER JOIN " +
                     "tblFacilities " +
                     "ON " +
                     "tblFacilityLocations.nFacilityID = tblFacilities.nFacilityID " +
                       "WHERE " +
                       "tblFacilityLocations.nFacilityLocationID <> @nFacilityLocationID AND " +
                        "tblFacilityLocations.sLocationName = @sLocationName";
                return await db.QueryAsync<T>(SqlString, new {@nFacilityLocationID = nFacilityLocationID, @sLocationName = sLocationName });
            }
        }

        public async Task<IEnumerable<T>> SelectRecordTypeBynFacilityID(int nFacilityID)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString =
                    "SELECT [lstRecordTypes].[nRecordTypeID]" +
                        ",[lnkFacilityRecordTypes].[sRecordTypeName]" +
                        ",[lstRecordTypes].[nWizardID]" +
                        ",[lstRecordTypes].[sNormalizedRecordTypeName]" +
                        ",[lstRecordTypes].[sFieldToolTip]" +
                        ",[lstRecordTypes].[dtLastUpdate] FROM " +
                        "[lstRecordTypes] " +
                        "inner join [lnkFacilityRecordTypes] " +
                        "on [lnkFacilityRecordTypes].nRecordTypeID = [lstRecordTypes].nRecordTypeID " +
                        "WHERE " +
                        "nFacilityID = @nFacilityID";
                return await db.QueryAsync<T>(SqlString, new { @nFacilityID = nFacilityID});
            }
        }

        public async Task<IEnumerable<T>> SelectSensitiveInfoBynFacilityID(int nFacilityID)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString =
                   " SELECT [lstSensitiveInfo].[nSensitiveInfoID]" +
                        ",[lnkFacilitySensitiveInfo].[sSensitiveInfoName]" +
                        ",[lstSensitiveInfo].[nWizardID]" +
                        ",[lstSensitiveInfo].[sNormalizedSensitiveInfoName]" +
                        ",[lstSensitiveInfo].[sFieldToolTip]" +
                        ",[lstSensitiveInfo].[dtLastUpdate] FROM " +
                        "[lstSensitiveInfo] " +
                        "INNER JOIN[lnkFacilitySensitiveInfo] " +
                        "ON [lnkFacilitySensitiveInfo].nSensitiveInfoID = [lstSensitiveInfo].nSensitiveInfoID " +
                        " WHERE nFacilityID = @nFacilityID";
                return await db.QueryAsync<T>(SqlString, new { @nFacilityID = nFacilityID });
            }
        }
        public async Task<IEnumerable<dynamic>> GetLocationByNormalizedName(int nFacilityID, int nFacilityLocationID, string sNormalizedLocationName)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString =
                   "SELECT * FROM " +
                     "tblFacilityLocations " +
                     "INNER JOIN " +
                     "tblFacilities " +
                     "ON " +
                     "tblFacilityLocations.nFacilityID = tblFacilities.nFacilityID " +
                       "WHERE " +
                       "tblFacilityLocations.sNormalizedLocationName = @sNormalizedLocationName AND " +
                       "tblFacilityLocations.nFacilityLocationID <> @nFacilityLocationID AND " +
                        "tblFacilities.nFacilityID = @nFacilityID";
                return await db.QueryAsync(SqlString, new { @nFacilityID = nFacilityID, @nFacilityLocationID = nFacilityLocationID, @sNormalizedLocationName  = sNormalizedLocationName });
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
        public async Task<IEnumerable<T>> GetLocationsList(int nFacilityID)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {

                string SqlString =
                    "SELECT " +
                      "nFacilityLocationID, " +
                      "sLocationName, " +
                      "sLocationAddress, " +
                      "sLocationCode, " +
                      "bLocationActiveStatus, " +
                      "sFacilityName, " +
                      "bIncludeInFacilityLevel " +
                      "FROM " +
                      "tblFacilityLocations " +
                      "INNER JOIN " +
                      "tblFacilities " +
                      "ON " +
                      "tblFacilityLocations.nFacilityID = tblFacilities.nFacilityID " +
                        "WHERE " +
                        "tblFacilities.nFacilityID = @nFacilityID";
                return await db.QueryAsync<T>(SqlString, new { @nFacilityID = nFacilityID });
            }
        }
        public async Task<IEnumerable<dynamic>> InnerJoin(string colA, string colB, string tblA, string tblB)
        {
            string SqlString = "SELECT * FROM " + tblA + " INNER JOIN " + tblB + " ON " + tblA + "." + colA + " = " + tblB + "." + colB;
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                return await db.QueryAsync(SqlString);
            }
        }

       
        public async Task<IEnumerable<T>> SelectListByInClause(int[] ids,string tblName, string colName,int nFacilityId)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString = "SELECT * FROM "+tblName +" WHERE nFacilityId="+ nFacilityId +"AND "+colName+" IN @ids";
                return await db.QueryAsync<T>(SqlString, new { @ids = ids });
            }
        }

        #endregion

        #region INSERT Queries
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
                    if (!Int32.TryParse(obj + "", out int nId))
                        return null;
                    Id = nId;
                }
            }
            return Id;
        }
        public async Task<bool> InsertMany(List<T> ourModels)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    long count = 0;
                    using (var trans = db.BeginTransaction())
                    {
                        count = db.Insert(ourModels, trans);
                        trans.Commit();
                    }
                    if (count > 0) { return true; }
                    else { return false; }
                }
                catch (Exception ex) {
                    return false;
                }
            }
        }
        #endregion

        #region Toggle/Delete Queries 
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
        public bool DeleteOneToMany(int id, string lnkTable)
        {
            using (var connection = new SqlConnection(sConnect))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var rowsAffected = connection.Execute(
                                         "DELETE " + lnkTable + @" 
                                    WHERE " + sKeyName + @" =@ID " +
                                         "DELETE " + sTableName + @" 
                                    WHERE " + sKeyName + @" =@ID",
                                         new { ID = id }, transaction);
                    }
                    catch (Exception ex) {
                        transaction.Rollback();
                        return false;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }
        #endregion

        #region Update Queries 
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
        public bool UpdateMany(List<T> ourModels)
        {
            try {
                using (SqlConnection db = new SqlConnection(sConnect))
                {
                    db.Open();
                    bool continueAhead = false;
                    continueAhead = db.Update(ourModels);
                    return continueAhead;
                }
            }
            catch (Exception ex) {
                return false;
            }
        }
        public async Task<int> UpdateRequesterFeedback(int nRequesterID, bool bRequestAnotherRecord, int nFeedbackRating, string sFeedbackComment, string sWizardName)
        {
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                string SqlString =
                    "UPDATE tblRequesters SET bRequestAnotherRecord = @bRequestAnotherRecord, " +
                      "nFeedbackRating = @nFeedbackRating, " +
                      "sFeedbackComment = @sFeedbackComment, " +
                      "sWizardName = @sWizardName " +
                        "WHERE " +
                        "tblRequesters.nRequesterID = @nRequesterID";
                await db.QueryAsync<T>(SqlString, new { @nRequesterID = nRequesterID, @bRequestAnotherRecord = bRequestAnotherRecord, @nFeedbackRating = nFeedbackRating, @sFeedbackComment = sFeedbackComment, @sWizardName = sWizardName });
                return nRequesterID;
            }
        }
        #endregion

        #region Stored Procedures
        public async Task<IEnumerable<dynamic>> EditFields(int ID, int nAdminUserID)
        {
            string SqlString = "spGetPatientFormBynFacilityID";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    IEnumerable<dynamic> a = await db.QueryAsync(SqlString, new { @nFacilityID = ID , @nAdminUserID = nAdminUserID }, commandType: CommandType.StoredProcedure);
                    return a;
                }
                catch (Exception ex) {
                    return (IEnumerable<dynamic>)ex;
                }
            }
        }
        public async Task<object> GetWizardConfigurationAsync(int nFacilityID, int nFacilityLocationID)
        {
            string SqlString = "spGetWizardConfigBynFacilityIdAndnFacilityLocationId";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                SqlMapper.GridReader wizardConfig = await db.QueryMultipleAsync(SqlString, new { @nFacilityID = nFacilityID, @nFacilityLocationID = nFacilityLocationID }, commandType: CommandType.StoredProcedure);
                var oFields = wizardConfig.Read().ToDictionary(row => (string)row.sNormalizedFieldName, row => (bool)row.bShow);
                var oPrimaryReason = wizardConfig.Read().ToList();
                var oRecordTypes = wizardConfig.Read().ToList();
                var oSensitiveInfo = wizardConfig.Read().ToList();
                var oShipmentTypes = wizardConfig.Read().ToList();
                var oWizardHelper = wizardConfig.Read().ToDictionary(row => (string)row.sWizardHelperName, row => (string)row.sWizardHelperValue);
                var oLocations = wizardConfig.Read().ToList();
                object newObject = new { oFields, oPrimaryReason, oRecordTypes, oSensitiveInfo, oShipmentTypes, oWizardHelper, oLocations };
                return newObject;
            }
        }
        public async Task<dynamic> GetLogoBackGroundforLocationsync(int nLocationID)
        {
            string SqlString = "spGetLogoAndBackgroundImageByLocationId";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                dynamic logoBackgroundLocation = await db.QueryFirstAsync(SqlString, new { @nLocationId = nLocationID }, commandType: CommandType.StoredProcedure);
                return logoBackgroundLocation;
            }
        }
        public async Task<object> GetLogoBackGroundforFacilityByGUIDAsync(string sGUID, string sLocationGUID)
        {
            string SqlString = "spGetLogoAndBackgroundImageforFacilityGUID";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                SqlMapper.GridReader logoBackgroundFacility = await db.QueryMultipleAsync(SqlString, new { @sGuid = sGUID, @sLocationGuid = sLocationGUID }, commandType: CommandType.StoredProcedure);
                var facilityLogoandBackground = logoBackgroundFacility.Read().ToList();
                var sWizards = logoBackgroundFacility.Read().Select(d => new object[] { d.sWizardName });
                List<string> soWizard = new List<string>();
                soWizard.Add("Wizard_01");
                foreach (var wiz in sWizards)
                {
                    soWizard.Add(wiz[0].ToString());
                }
                String[] oWizards = soWizard.ToArray();
                var wizardHelper = logoBackgroundFacility.Read().ToDictionary(row => (string)row.sWizardHelperName, row => (string)row.sWizardHelperValue);
                var locationDetails = logoBackgroundFacility.Read().ToList();
                var wizardsSave = logoBackgroundFacility.Read().ToDictionary(row => (string)row.sWizardName, row => (int)row.bSavetoRequester);
                object newObject = new { facilityLogoandBackground, oWizards, wizardHelper, locationDetails, wizardsSave };
                return newObject;
            }
        }
        public void AddDependencyRecordsForFacility(int nFacilityID, int nConnectionID, int nAdminUserId)
        {
            string SqlString = "spAddDependencyRecordsForFacility";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    db.Query(SqlString, new { @nFacilityID = nFacilityID, @nConnectionID = nConnectionID, @nAdminUserId = nAdminUserId }, trans, commandType: CommandType.StoredProcedure);
                    trans.Commit();
                }
            }
        }
        public void AddDependencyRecordsForFacilityLocation(int nFacilityLocationID,int nFacilityID)
        {
            string SqlString = "spAddDependencyRecordsForFacilityLocation";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    db.Query(SqlString, new { @nFacilityID = nFacilityID, @nFacilityLocationID = nFacilityLocationID}, trans, commandType: CommandType.StoredProcedure);
                    trans.Commit();
                }
            }
        }
        public async Task<int> GetAdminUserID(string sName, string sUPN, string sEmail) {
            string SqlString = "spGetAdminUserID";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                int nAdminID = await db.QueryFirstAsync<int>(SqlString, new { @sName = sName, @sUPN= sUPN, @sEmail= sEmail }, commandType: CommandType.StoredProcedure);
                return nAdminID;
            }
        }

        public async Task<string> GetConnectionStringByFacilityID(int nFacilityId)
        {
            string SqlString = "spGetConnectionStringByFacilityId";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                string sConnectionString = await db.QueryFirstAsync<string>(SqlString, new { @nFacilityID = nFacilityId }, commandType: CommandType.StoredProcedure);
                return sConnectionString;
            }
        }

        public async Task<string> GetNormalizedNameByMasterName(string sMasterName)
        {
            string SqlString = "spGetNormalizedNameByMasterName";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                db.Open();
                string sNormalizedName = await db.QueryFirstAsync<string>(SqlString, new { @sMasterName = sMasterName }, commandType: CommandType.StoredProcedure);
                return sNormalizedName;
            }
        }

        public async Task<IEnumerable<dynamic>> EditDisclaimers(int ID)
        {
            string SqlString = "spGetDisclaimerInfoBynFacilityID";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    IEnumerable<dynamic> a = await db.QueryAsync(SqlString, new { @nFacilityID = ID }, commandType: CommandType.StoredProcedure);
                    return a;
                }
                catch (Exception ex)
                {
                    return (IEnumerable<dynamic>)ex;
                }
            }
        }

        public async Task<IEnumerable<dynamic>> GetAuditData(string sStart= null,string sEnd=null, string sFacilityName = null, string sLocationName = null, string sAdminName = null)
        {
            string SqlString = "spGetAuditReport";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    IEnumerable<dynamic> a = await db.QueryAsync(SqlString, new { @sStart = sStart, @sEnd = sEnd, @sFacilityName = sFacilityName, @sLocationName = sLocationName , @sAdminName = sAdminName }, commandType: CommandType.StoredProcedure);
                    return a;
                }
                catch (Exception ex)
                {
                    return (IEnumerable<dynamic>)ex;
                }
            }
        }

        public async Task<IEnumerable<dynamic>> GetFacilityLocationData(string sStart = null, string sEnd = null, string sFacilityName = null, string sLocationName = null)
        {
            string SqlString = "spGetFacilityLocationReport";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    IEnumerable<dynamic> a = await db.QueryAsync(SqlString, new { @sStart = sStart, @sEnd = sEnd, @sFacilityName = sFacilityName, @sLocationName = sLocationName }, commandType: CommandType.StoredProcedure);
                    return a;
                }
                catch (Exception ex)
                {
                    return (IEnumerable<dynamic>)ex;
                }
            }
        }

        public async Task<IEnumerable<dynamic>> GetFacilityConfigurationData(string sFacilityName = null, string sWizardName = null)
        {
            string SqlString = "spGetFacilityConfigurationReport";
            using (SqlConnection db = new SqlConnection(sConnect))
            {
                try
                {
                    db.Open();
                    IEnumerable<dynamic> a = await db.QueryAsync(SqlString, new { @sFacilityName = sFacilityName, @sWizardName = sWizardName }, commandType: CommandType.StoredProcedure);
                    return a;
                }
                catch (Exception ex)
                {
                    return (IEnumerable<dynamic>)ex;
                }
            }
        }
        #endregion
    }
}
