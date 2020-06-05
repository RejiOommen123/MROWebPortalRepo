﻿using Microsoft.EntityFrameworkCore;
using MROWebApi.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public static class ModelBuilderExtension
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblROIFacilities>().HasData(
                new tblROIFacilities
                {
                    nROIFacilityID = 1,
                    sFacilityName = "Cleveland Clinic",
                    sDescription = "Info about Cleveland",
                    sSMTPUsername = "Cleveland101",
                    sSMTPPassword = "Cleveland@101",
                    sSMTPUrl = "smtp.cleveland.com",
                    sFTPUsername = "Cleveland101",
                    sFTPPassword = "Cleveland@101",
                    sFTPUrl = "ftp://ftp.cleveland.com/",
                    bActiveStatus = true,
                    sOutboundEmail = "noreply@cleveland.com",
                    sConfigShowFields = "TestFields",
                    sConfigShowWizards = "Test Wizards",
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now               
                }
            );
            modelBuilder.Entity<lstWizards>().HasData(
                    new lstWizards
                    {
                        nWizardID = 1,
                        sWizardName = "Wizard-1",
                    },
                    new lstWizards
                    {
                        nWizardID = 2,
                        sWizardName = "Wizard-2"
                    },
                    new lstWizards
                    {
                        nWizardID = 3,
                        sWizardName = "Wizard-3"
                    },
                    new lstWizards
                    {
                        nWizardID = 4,
                        sWizardName = "Wizard-4"
                    },
                    new lstWizards
                    {
                        nWizardID = 5,
                        sWizardName = "Wizard-5"
                    },
                    new lstWizards
                    {
                        nWizardID = 6,
                        sWizardName = "Wizard-6"
                    },
                    new lstWizards
                    {
                        nWizardID = 7,
                        sWizardName = "Wizard-7"
                    },
                    new lstWizards
                    {
                        nWizardID = 8,
                        sWizardName = "Wizard-8"
                    },
                    new lstWizards
                    {
                        nWizardID = 9,
                        sWizardName = "Wizard-9"
                    },
                    new lstWizards
                    {
                        nWizardID = 10,
                        sWizardName = "Wizard-10"
                    },
                    new lstWizards
                    {
                        nWizardID = 11,
                        sWizardName = "Wizard-11"
                    },
                    new lstWizards
                    {
                        nWizardID = 12,
                        sWizardName = "Wizard-12"
                    },
                    new lstWizards
                    {
                        nWizardID = 13,
                        sWizardName = "Wizard-13"
                    },
                    new lstWizards
                    {
                        nWizardID = 14,
                        sWizardName = "Wizard-14"
                    }
                );
            modelBuilder.Entity<lstFields>().HasData(
                new lstFields
                {
                    nFieldID = 1,
                    nWizardID = 2,
                    sFieldName = "Selected Location",
                    sNormalizedFieldName = "SelectedLocation"
                },
                new lstFields
                {
                    nFieldID = 2,
                    nWizardID = 3,
                    sFieldName = "Are you Patient",
                    sNormalizedFieldName = "AreyouPatient"
                },
                 new lstFields
                 {
                     nFieldID = 3,
                     nWizardID = 4,
                     sFieldName = "Patient Email Id",
                     sNormalizedFieldName = "PEmailId"
                 },
                new lstFields
                {
                    nFieldID = 4,
                    nWizardID = 4,
                    sFieldName = "Confirm Report",
                    sNormalizedFieldName = "ConfirmReport"
                },
                new lstFields
                {
                    nFieldID = 5,
                    nWizardID = 5,
                    sFieldName = "Patient First Name",
                    sNormalizedFieldName = "PFName"
                },
                new lstFields
                {
                    nFieldID = 6,
                    nWizardID = 5,
                    sFieldName = "Patient Middle Initial",
                    sNormalizedFieldName = "PMInitial"
                },
                new lstFields
                {
                    nFieldID = 7,
                    nWizardID = 5,
                    sFieldName = "Patient Last Name",
                    sNormalizedFieldName = "PLName"
                },
                new lstFields
                {
                    nFieldID = 8,
                    nWizardID = 5,
                    sFieldName = "Is Patient Deceased",
                    sNormalizedFieldName = "IsPatientDeceased"
                },
                new lstFields
                {
                    nFieldID = 9,
                    nWizardID = 6,
                    sFieldName = "Zip Code",
                    sNormalizedFieldName = "ZipCode"
                },
                new lstFields
                {
                    nFieldID = 10,
                    nWizardID = 7,
                    sFieldName = "Street Area",
                    sNormalizedFieldName = "StreetArea"
                },
                 new lstFields
                 {
                     nFieldID = 11,
                     nWizardID = 8,
                     sFieldName = "Patient Birth Date",
                     sNormalizedFieldName = "PBirthDate"
                 },
                 new lstFields
                 {
                     nFieldID = 12,
                     nWizardID = 9,
                     sFieldName = "Primary Reason",
                     sNormalizedFieldName = "PrimaryReason"
                 },
                 new lstFields
                 {
                     nFieldID = 13,
                     nWizardID = 10,
                     sFieldName = "TimeFrame Date Range",
                     sNormalizedFieldName = "TFDateRange"
                 },
                 new lstFields
                 {
                     nFieldID = 14,
                     nWizardID = 11,
                     sFieldName = "Record Type",
                     sNormalizedFieldName = "RecordType"
                 },
                 new lstFields
                 {
                     nFieldID = 15,
                     nWizardID = 12,
                     sFieldName = "Sensitive Info",
                     sNormalizedFieldName = "SensitiveInfo"
                 },
                 new lstFields
                 {
                     nFieldID = 16,
                     nWizardID = 13,
                     sFieldName = "Authorization Expire",
                     sNormalizedFieldName = "AuthExpire"
                 },
                 new lstFields
                 {
                     nFieldID = 17,
                     nWizardID = 14,
                     sFieldName = "Authorization Expire",
                     sNormalizedFieldName = "AuthExpire"
                 }
             );

            modelBuilder.Entity<lnkROIFacilityFieldMaps>().HasData(
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 1,
                   nROIFacilityID = 1,
                   nFieldID = 1,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 2,
                   nROIFacilityID = 1,
                   nFieldID = 2,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 3,
                   nROIFacilityID = 1,
                   nFieldID = 3,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 4,
                   nROIFacilityID = 1,
                   nFieldID = 4,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 5,
                   nROIFacilityID = 1,
                   nFieldID = 5,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 6,
                   nROIFacilityID = 1,
                   nFieldID = 6,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 7,
                   nROIFacilityID = 1,
                   nFieldID = 7,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 8,
                   nROIFacilityID = 1,
                   nFieldID = 8,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
                new lnkROIFacilityFieldMaps
                {
                    nROIFacilityFieldMapID = 9,
                    nROIFacilityID = 1,
                    nFieldID = 9,
                    bShow = true,
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 10,
                   nROIFacilityID = 1,
                   nFieldID = 10,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
               new lnkROIFacilityFieldMaps
               {
                   nROIFacilityFieldMapID = 11,
                   nROIFacilityID = 1,
                   nFieldID = 11,
                   bShow = true,
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               }
           );
            modelBuilder.Entity<lstPrimaryReasons>().HasData(
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 1,
                    sPrimaryReasonName = "Continued Care"
                },
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 2,
                    sPrimaryReasonName = "Patient Request"
                },
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 3,
                    sPrimaryReasonName = "Insurance"
                },
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 4,
                    sPrimaryReasonName = "Social Security Benifits/Claims"
                },
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 5,
                    sPrimaryReasonName = "OtherReason"
                },
                new lstPrimaryReasons
                {
                    nPrimaryReasonID = 6,
                    sPrimaryReasonName = "Continued Care"
                }
            );
            modelBuilder.Entity<lnkROIFacilityPrimaryReasons>().HasData(
               new lnkROIFacilityPrimaryReasons
               {
                   nROIFacilityPrimaryReasonID = 1,                  
                   nPrimaryReasonID = 1,
                   nROIFacilityID = 1,
                   sPrimaryReasonName = "Continued Care",
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               },
                new lnkROIFacilityPrimaryReasons
                {
                    nROIFacilityPrimaryReasonID = 2,
                    nPrimaryReasonID = 2,
                    nROIFacilityID = 1,
                    sPrimaryReasonName = "Patient Request",
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                },
                 new lnkROIFacilityPrimaryReasons
                 {
                     nROIFacilityPrimaryReasonID = 3,
                     nPrimaryReasonID = 3,
                     nROIFacilityID = 1,
                     sPrimaryReasonName = "Insurance",
                     sCreatedBy = 1,
                     dtCreatedDate = DateTime.Now,
                     sUpdatedBy = 1,
                     dtUpdatedDate = DateTime.Now
                 },
                  new lnkROIFacilityPrimaryReasons
                  {
                      nROIFacilityPrimaryReasonID = 4,
                      nPrimaryReasonID = 4,
                      nROIFacilityID = 1,
                      sPrimaryReasonName = "Social Security Benifits/Claims",
                      sCreatedBy = 1,
                      dtCreatedDate = DateTime.Now,
                      sUpdatedBy = 1,
                      dtUpdatedDate = DateTime.Now
                  },
                   new lnkROIFacilityPrimaryReasons
                   {
                       nROIFacilityPrimaryReasonID = 5,
                       nPrimaryReasonID = 5,
                       nROIFacilityID = 1,
                       sPrimaryReasonName = "OtherReason",
                       sCreatedBy = 1,
                       dtCreatedDate = DateTime.Now,
                       sUpdatedBy = 1,
                       dtUpdatedDate = DateTime.Now
                   },
                    new lnkROIFacilityPrimaryReasons
                    {
                        nROIFacilityPrimaryReasonID = 6,
                        nPrimaryReasonID = 6,
                        nROIFacilityID = 1,
                        sPrimaryReasonName = "Continued Care",
                        sCreatedBy = 1,
                        dtCreatedDate = DateTime.Now,
                        sUpdatedBy = 1,
                        dtUpdatedDate = DateTime.Now
                    }
           );
            modelBuilder.Entity<lstRecordTypes>().HasData(
              new lstRecordTypes
              {
                  nRecordTypeID = 1,
                  sRecordTypeName = "Abstract"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 2,
                  sRecordTypeName = "Discharge Summary"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 3,
                  sRecordTypeName = "Operative Reports"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 4,
                  sRecordTypeName = "History and Physical"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 5,
                  sRecordTypeName = "Laboratory Report"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 6,
                  sRecordTypeName = "Radiology Report"
              },
              new lstRecordTypes
              {
                  nRecordTypeID = 7,
                  sRecordTypeName = "Other"
              }
          );
            modelBuilder.Entity<lnkROIFacilityRecordTypes>().HasData(
              new lnkROIFacilityRecordTypes
              {
                 nROIFacilityRecordTypeID = 1,
                 nRecordTypeID = 1,
                 nROIFacilityID = 1,
                 sRecordTypeName = "Abstract",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID = 2,
                  nRecordTypeID = 2,
                  nROIFacilityID = 1,
                  sRecordTypeName = "Discharge Summary",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID = 3,
                  nRecordTypeID = 3,
                  nROIFacilityID = 1,
                  sRecordTypeName = "Operative Reports",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID =4,
                  nRecordTypeID = 4,
                  nROIFacilityID = 1,
                  sRecordTypeName = "History and Physical",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID = 5,
                  nRecordTypeID = 5,
                  nROIFacilityID = 1,
                  sRecordTypeName = "Laboratory Report",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID = 6,
                  nRecordTypeID = 6,
                  nROIFacilityID = 1,
                  sRecordTypeName = "Radiology Report",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
              new lnkROIFacilityRecordTypes
              {
                  nROIFacilityRecordTypeID = 7,
                  nRecordTypeID = 7,
                  nROIFacilityID = 1,
                  sRecordTypeName = "Other",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              }
          );
            modelBuilder.Entity<lstSensitiveInfo>().HasData(
              new lstSensitiveInfo
              {
                  nSensitiveInfoID = 1,
                  sSensitiveInfoName = "HIV Test Results"
              },
              new lstSensitiveInfo
              {
                  nSensitiveInfoID = 2,
                  sSensitiveInfoName = "Behavioural/Mental Health Records"
              },
              new lstSensitiveInfo
              {
                  nSensitiveInfoID = 3,
                  sSensitiveInfoName = "Substance Abuse Information"
              },
              new lstSensitiveInfo
              {
                  nSensitiveInfoID = 4,
                  sSensitiveInfoName = "Sexually Transmitted Dieases"
              }
          );
            modelBuilder.Entity<lnkROIFacilitySensitiveInfo>().HasData(
             new lnkROIFacilitySensitiveInfo
             {
                 nROIFacilitySensitiveInfoID = 1, 
                 nSensitiveInfoID = 1,
                 nROIFacilityID = 1,
                 sSensitiveInfoName= "HIV Test Results",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
             },
              new lnkROIFacilitySensitiveInfo
             {
                 nROIFacilitySensitiveInfoID = 2, 
                 nSensitiveInfoID = 2,
                 nROIFacilityID = 1,
                 sSensitiveInfoName= "Behavioural/Mental Health Records",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
             },
               new lnkROIFacilitySensitiveInfo
             {
                 nROIFacilitySensitiveInfoID = 3, 
                 nSensitiveInfoID = 3,
                 nROIFacilityID = 1,
                 sSensitiveInfoName= "Substance Abuse Information",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
             },
                new lnkROIFacilitySensitiveInfo
             {
                 nROIFacilitySensitiveInfoID = 4, 
                 nSensitiveInfoID = 4,
                 nROIFacilityID = 1,
                 sSensitiveInfoName= "Sexually Transmitted Dieases",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
             }
         );
            modelBuilder.Entity<lstWayOfSendRecord>().HasData(
              new lstWayOfSendRecord
              {
                  nWayOfSendRecordID = 1,
                  sWayOfSendRecordName = "Patient Portal"
              },
              new lstWayOfSendRecord
              {
                  nWayOfSendRecordID = 2,
                  sWayOfSendRecordName = "Secure Email"
              },
              new lstWayOfSendRecord
              {
                  nWayOfSendRecordID = 3,
                  sWayOfSendRecordName = "Email"
              },
              new lstWayOfSendRecord
              {
                  nWayOfSendRecordID = 4,
                  sWayOfSendRecordName = "In-Person"
              }
          );
            modelBuilder.Entity<lnkROIFacilityWayOfSendRecord>().HasData(
            new lnkROIFacilityWayOfSendRecord
            {
                nROIFacilityWayOfSendRecordID = 1,
                nWayOfSendRecordID = 1,
                nROIFacilityID = 1,
                sWayOfSendRecordName = "Patient Portal",
                sCreatedBy = 1,
                dtCreatedDate = DateTime.Now,
                sUpdatedBy = 1,
                dtUpdatedDate = DateTime.Now
            },
             new lnkROIFacilityWayOfSendRecord
             {
                 nROIFacilityWayOfSendRecordID = 2,
                 nWayOfSendRecordID = 2,
                 nROIFacilityID = 1,
                 sWayOfSendRecordName = "Secure Email",
                 sCreatedBy = 1,
                 dtCreatedDate = DateTime.Now,
                 sUpdatedBy = 1,
                 dtUpdatedDate = DateTime.Now
             },
              new lnkROIFacilityWayOfSendRecord
              {
                  nROIFacilityWayOfSendRecordID = 3,
                  nWayOfSendRecordID = 3,
                  nROIFacilityID = 1,
                  sWayOfSendRecordName = "Email",
                  sCreatedBy = 1,
                  dtCreatedDate = DateTime.Now,
                  sUpdatedBy = 1,
                  dtUpdatedDate = DateTime.Now
              },
               new lnkROIFacilityWayOfSendRecord
               {
                   nROIFacilityWayOfSendRecordID = 4,
                   nWayOfSendRecordID = 4,
                   nROIFacilityID = 1,
                   sWayOfSendRecordName = "In-Person",
                   sCreatedBy = 1,
                   dtCreatedDate = DateTime.Now,
                   sUpdatedBy = 1,
                   dtUpdatedDate = DateTime.Now
               }
        );
        modelBuilder.Entity<lnkROIFacilityConnection>().HasData(
                new lnkROIFacilityConnection
                {
                    nROIFacilityConnectionID = 1,
                    nROIFacilityID = 1,
                    sGUID="Test GUID",
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                }
            );
            modelBuilder.Entity<lnkROIFacilityLocations>().HasData(
                new lnkROIFacilityLocations
                {
                    nLocationID = 1,
                    nROIFacilityID = 1,
                    sLocationCode = "101",
                    sLocationName = "Cleverland Clinic",
                    sLocationAddress = "Cleverland Clinic Address",
                    sFaxNo = "1234567890",
                    sPhoneNo = "1234567890",
                    sConfigLogoName = "MRO-Web.png",
                    sConfigLogoData = "data:image/png;base64,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",
                    sConfigBackgroundName= "hospitalbg.jpg",
                    sConfigBackgroundData= "data:image/jpeg;base64,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",
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                },
                   new lnkROIFacilityLocations
                   {
                       nLocationID = 2,
                       nROIFacilityID = 1,
                       sLocationCode = "102",
                       sLocationName = "Cleverland Hospital",
                       sLocationAddress = "Cleverland Hospital Address",
                       sFaxNo = "1234567890",
                       sPhoneNo = "1234567890",
                       sConfigLogoName = "MRO-Web.png",
                       sConfigLogoData = "data:image/png;base64,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",
                       sConfigBackgroundName = "hospitalbg.jpg",
                       sConfigBackgroundData = "data:image/jpeg;base64,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",
                       sCreatedBy = 1,
                       dtCreatedDate = DateTime.Now,
                       sUpdatedBy = 1,
                       dtUpdatedDate = DateTime.Now
                   }
            );
        }
    }
}
