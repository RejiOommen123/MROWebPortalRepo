using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MROWebApi.Services
{
    //public class DataProtectionPurposeStrings
    //{
    //    #region Data Protection Key
    //    public readonly string Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //    #endregion
    //}
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
    }
}
