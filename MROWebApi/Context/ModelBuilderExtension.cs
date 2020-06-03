using Microsoft.EntityFrameworkCore;
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
                    sNormalizedFieldName = "NotPatient"
                },
                 new lstFields
                 {
                     nFieldID = 3,
                     nWizardID = 4,
                     sFieldName = "Email Id",
                     sNormalizedFieldName = "EmailID"
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
                    sFieldName = "First Name",
                    sNormalizedFieldName = "FName"
                },
                new lstFields
                {
                    nFieldID = 6,
                    nWizardID = 5,
                    sFieldName = "Middle Initial",
                    sNormalizedFieldName = "MInitial"
                },
                new lstFields
                {
                    nFieldID = 7,
                    nWizardID = 5,
                    sFieldName = "Last Name",
                    sNormalizedFieldName = "LName"
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
                    sFieldName = "Postal Code",
                    sNormalizedFieldName = "PostalCode"
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
                     sFieldName = "Birth Date",
                     sNormalizedFieldName = "BDay"
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
                    sLocationCode = 101,
                    sLocationName = "Cleverland Clinic",
                    sLocationAddress = "Cleverland Clinic Address",
                    nFaxNo = 4026,
                    nPhoneNo = 4026,
                    sCreatedBy = 1,
                    dtCreatedDate = DateTime.Now,
                    sUpdatedBy = 1,
                    dtUpdatedDate = DateTime.Now
                },
                   new lnkROIFacilityLocations
                   {
                       nLocationID = 2,
                       nROIFacilityID = 1,
                       sLocationCode = 102,
                       sLocationName = "Cleverland Hospital",
                       sLocationAddress = "Cleverland Hospital Address",
                       nFaxNo = 4026,
                       nPhoneNo = 4026,
                       sCreatedBy = 1,
                       dtCreatedDate = DateTime.Now,
                       sUpdatedBy = 1,
                       dtUpdatedDate = DateTime.Now
                   }
            );
        }
    }
}
