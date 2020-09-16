using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MROWebApi.Services
{
    public  class MROLogger
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
        public  void LogAdminRecords(int nAdminUserID,string sDescription,string sEventName,string sModuleName)
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
            catch
            {

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

        public static void LogExceptionRecords(string sStatusName, string sModuleName, string sDescription, DBConnectionInfo _info)
        {
            try
            {
                ExceptionLoggerRepository exceptionLoggerRepository = new ExceptionLoggerRepository(_info);
                ExceptionLogger exceptionLoggerDetails = new ExceptionLogger()
                {
                    sStatusName = sStatusName,
                    sModuleName = sModuleName,
                    sDescription = sDescription,
                    dtExceptionTime = DateTime.Now
                };
                exceptionLoggerRepository.Insert(exceptionLoggerDetails);
            }
            catch
            {

            }

        }

        #endregion

        #region Encrypt Decrypt Strings

        /// <summary>
        /// Decrypts a Given String
        /// </summary>
        /// <param name="cipherText">Encrypted String</param>
        /// <returns>string - Decrypted String</returns>
        public string DecryptString(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Encrypts a Given String
        /// </summary>
        /// <param name="encryptString">Normal String</param>
        /// <returns>string - Encrypted String</returns>
        public string EncryptString(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }
        #endregion

        #region Get new/old value
        public void UpdateAuditSingle<T>(T oldObject, T newObject,AdminModuleLogger adminModuleLogger)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            AdminModuleLogger result = new AdminModuleLogger();
            string combineNewData="";
            string combineOldData = "";
            AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(IgnorePropertyCompareAttribute)))
                {
                    continue;
                }

                object oldValue = pi.GetValue(oldObject), newValue = pi.GetValue(newObject);

                if (!object.Equals(oldValue, newValue))
                {
                    combineNewData = combineNewData + "{" + pi.Name + " : " + newValue + "}, ";
                    combineOldData = combineOldData + "{" + pi.Name + " : " + oldValue + "}, ";
                }
            }
            if (string.IsNullOrEmpty(combineOldData))
            {
                if (combineOldData.EndsWith(", "))
                {
                    combineOldData = combineOldData.Remove(combineOldData.Length - 2, 2);
                }
            }
            if (string.IsNullOrEmpty(combineNewData))
            {
                if (combineNewData.EndsWith(", "))
                {
                    combineNewData = combineNewData.Remove(combineNewData.Length - 2, 2);
                }
            }

            result.nAdminUserID = adminModuleLogger.nAdminUserID;
            result.sEventName = adminModuleLogger.sEventName;
            result.sModuleName = adminModuleLogger.sModuleName;
            result.sDescription = adminModuleLogger.sDescription;
            result.sNewValue = combineNewData;
            result.sOldValue = combineOldData;
            result.dtLogTime = DateTime.Now;

            adminModuleLoggerRepository.Insert(result);
        }

        public void UpdateAuditMany<T>(List<T> oldObjectList, List<T> newObjectList, AdminModuleLogger adminModuleLogger, string id)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<AdminModuleLogger> result = new List<AdminModuleLogger>();
            string combineNewData = "";
            string combineOldData = "";
            AdminModuleLoggerRepository adminModuleLoggerRepository = new AdminModuleLoggerRepository(_info);

            foreach (T newObject in newObjectList)
            {
                T oldObject = oldObjectList.FirstOrDefault(q => q.GetType().GetProperty(id).GetValue(q, null).Equals(newObject.GetType().GetProperty(id).GetValue(newObject, null)));
                //T oldObject = oldObjectList.Where(newObject);
                foreach (PropertyInfo pi in properties)
                {
                    if (pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(IgnorePropertyCompareAttribute)))
                    {
                        continue;
                    }                    
                    object oldValue = pi.GetValue(oldObject), newValue = pi.GetValue(newObject);

                    if (!object.Equals(oldValue, newValue))
                    {
                        combineNewData = combineNewData + "{" + pi.Name + " : " + newValue + "}, ";
                        combineOldData = combineOldData + "{" + pi.Name + " : " + oldValue + "}, ";
                    }
                }
                if (string.IsNullOrEmpty(combineOldData))
                {
                    if (combineOldData.EndsWith(", "))
                    {
                        combineOldData = combineOldData.Remove(combineOldData.Length - 2, 2);
                    }
                }
                if (string.IsNullOrEmpty(combineNewData))
                {
                    if (combineNewData.EndsWith(", "))
                    {
                        combineNewData = combineNewData.Remove(combineNewData.Length - 2, 2);
                    }
                }

                result.Add(new AdminModuleLogger
                {
                    nAdminUserID = adminModuleLogger.nAdminUserID,
                    sEventName = adminModuleLogger.sEventName,
                    sModuleName = adminModuleLogger.sModuleName,
                    sDescription = adminModuleLogger.sDescription,
                    sNewValue = combineNewData,
                    sOldValue = combineOldData,
                    dtLogTime = DateTime.Now
                });
                combineNewData = "";
                combineOldData = "";
            }

            adminModuleLoggerRepository.InsertMany(result);
        }
        #endregion
    }
}
