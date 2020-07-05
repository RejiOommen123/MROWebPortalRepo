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
        /// <param name="nRequesterID">Requester Unique ID</param>
        /// <param name="nFacilityID">Facility Unique ID</param>
        /// <param name="sDescription">Description about Event</param>
        /// <param name="sWizardName">Wizard Name</param>
        public void LogRequesterRecords(int nRequesterID,int nFacilityID, string sDescription, string sWizardName) {
            RequesterModuleLoggerRepository patientModuleLoggerRepository = new RequesterModuleLoggerRepository(_info);
            RequesterModuleLogger logRequesterDetails = new RequesterModuleLogger()
            {
                nRequesterID = nRequesterID,
                sDescription = sDescription,
                nFacilityID = nFacilityID,
                sWizardName = sWizardName,
                dtLogTime = DateTime.Now
            };
            patientModuleLoggerRepository.Insert(logRequesterDetails);
        }
        #endregion
    }
}
