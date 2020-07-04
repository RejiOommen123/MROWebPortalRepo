using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System;

namespace MROWebApi.Services
{
    public class DataProtectionPurposeStrings
    {
        #region Data Protection Key
        public readonly string Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        #endregion
    }
    public class MROLogger
    {
        #region Constructor
        private DBConnectionInfo _info;
        public MROLogger(DBConnectionInfo info) {
            _info = info;
        }
        #endregion

        #region Logger Methods

        /// <summary>
        /// Admin Module Logger Method
        /// </summary>
        /// <param name="nAdminUserID">Admin User ID</param>
        /// <param name="sDescription">Description of Event</param>
        /// <param name="sEventName">Event Name</param>
        /// <param name="sModuleName">Module Name</param>
        public void LogAdminRecords(int nAdminUserID,string sDescription,string sEventName,string sModuleName)
        {
            try {
                AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
                AdminModuleLogger logAdminDetails = new AdminModuleLogger()
                {
                    nAdminUserID = nAdminUserID,
                    sDescription = sDescription,
                    sEventName = sEventName,
                    sModuleName = sModuleName,
                    dtLogTime = DateTime.Now
                };
                adminModuleLoggerRepository.Insert(logAdminDetails);
            }
            catch (Exception ex) {

            }

        }
        /// <summary>
        /// Requester Module Logger Method
        /// </summary>
        /// <param name="nRequesterID">Requester ID</param>
        /// <param name="sFacilityGUID">Facility GUID</param>
        /// <param name="sUserIPAddress">Requester IP Address</param>
        /// <param name="sWizardName">Name of Wizard</param>
        public void LogRequesterRecords(int nRequesterID,string sFacilityGUID,string sUserIPAddress, string sWizardName) {
            RequesterModuleLoggerRepository patientModuleLoggerRepository = new RequesterModuleLoggerRepository(_info);
            RequesterModuleLogger logRequesterDetails = new RequesterModuleLogger()
            {
                nRequesterID = nRequesterID,
                sFacilityGUID = sFacilityGUID,
                sUserIPAddress = sUserIPAddress,
                sWizardName = sWizardName,
                dtLogTime = DateTime.Now
            };
            patientModuleLoggerRepository.Insert(logRequesterDetails);
        }
        #endregion
    }
}
