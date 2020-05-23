using Microsoft.EntityFrameworkCore;
using MROPOC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MROWebAPI.Context
{
    public static class ModelBuilderExtension
    {
        public static void seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser
                {
                    AdminUserId = 1,
                    Email = "admin@razor-tech.com",
                    Password = "admin"
                }
            );
            modelBuilder.Entity<Wizard>().HasData(
                    new Wizard
                    {
                        WizardId = 1,
                        WizardName = "Wizard-1"
                    },
                    new Wizard
                    {
                        WizardId = 2,
                        WizardName = "Wizard-2"
                    },
                    new Wizard
                    {
                        WizardId = 3,
                        WizardName = "Wizard-3"
                    },
                    new Wizard
                    {
                        WizardId = 4,
                        WizardName = "Wizard-4"
                    },
                    new Wizard
                    {
                        WizardId = 5,
                        WizardName = "Wizard-5"
                    },
                    new Wizard
                    {
                        WizardId = 6,
                        WizardName = "Wizard-6"
                    },
                    new Wizard
                    {
                        WizardId = 7,
                        WizardName = "Wizard-7"
                    },
                    new Wizard
                    {
                        WizardId = 8,
                        WizardName = "Wizard-8"
                    }
                );
            modelBuilder.Entity<Field>().HasData(
                new Field
                {
                    FieldId = 1,
                    WizardId = 2,
                    FieldName = "Selected Location",
                    NormalizedFieldName = "SelectedLocation"
                },
                new Field
                {
                    FieldId = 2,
                    WizardId = 3,
                    FieldName = "Are you Patient",
                    NormalizedFieldName = "NotPatient"
                },
                 new Field
                 {
                     FieldId = 3,
                     WizardId = 4,
                     FieldName = "Email Id",
                     NormalizedFieldName = "EmailID"
                 },
                new Field
                {
                    FieldId = 4,
                    WizardId = 4,
                    FieldName = "Confirm Report",
                    NormalizedFieldName = "ConfirmReport"
                },
                new Field
                {
                    FieldId = 5,
                    WizardId = 5,
                    FieldName = "First Name",
                    NormalizedFieldName = "FName"
                },
                new Field
                {
                    FieldId = 6,
                    WizardId = 5,
                    FieldName = "Middle Initial",
                    NormalizedFieldName = "MInitial"
                },
                new Field
                {
                    FieldId = 7,
                    WizardId = 5,
                    FieldName = "Last Name",
                    NormalizedFieldName = "LName"
                },
                new Field
                {
                    FieldId = 8,
                    WizardId = 5,
                    FieldName = "Is Patient Deceased",
                    NormalizedFieldName = "IsPatientDeceased"
                },
                new Field
                {
                    FieldId = 9,
                    WizardId = 6,
                    FieldName = "Postal Code",
                    NormalizedFieldName = "PostalCode"
                },
                new Field
                {
                    FieldId = 10,
                    WizardId = 7,
                    FieldName = "Street Area",
                    NormalizedFieldName = "StreetArea"
                },
                 new Field
                 {
                     FieldId = 11,
                     WizardId = 8,
                     FieldName = "Birth Date",
                     NormalizedFieldName = "BDay"
                 }
             );
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Cleveland Clinic",
                    CustomerAddress = "Cleveland",
                    Description = "Info about Cleveland",
                    NumOfInstitution = "Cleveland Clinic,Cleveland Hospital",
                    SMTPUsername = "Cleveland101",
                    SMTPPassword = "Cleveland@101",
                    SMTPUrl = "smtp.cleveland.com",
                    FTPUsername = "Cleveland101",
                    FTPPassword = "Cleveland@101",
                    FTPUrl = "ftp://ftp.cleveland.com/",
                    ConfigFileUrl = "https://www.cleveland.com/data",
                    ActiveStatus = true
                }
            );
            modelBuilder.Entity<FieldCustomerMap>().HasData(
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 1,
                   CustomerId = 1,
                   FieldId = 1,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 2,
                   CustomerId = 1,
                   FieldId = 2,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 3,
                   CustomerId = 1,
                   FieldId = 3,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 4,
                   CustomerId = 1,
                   FieldId = 4,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 5,
                   CustomerId = 1,
                   FieldId = 5,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 6,
                   CustomerId = 1,
                   FieldId = 6,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 7,
                   CustomerId = 1,
                   FieldId = 7,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 8,
                   CustomerId = 1,
                   FieldId = 8,
                   IsEnable = true
               },
                new FieldCustomerMap
                {
                    FieldCustomerMapId = 9,
                    CustomerId = 1,
                    FieldId = 9,
                    IsEnable = true
                },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 10,
                   CustomerId = 1,
                   FieldId = 10,
                   IsEnable = true
               },
               new FieldCustomerMap
               {
                   FieldCustomerMapId = 11,
                   CustomerId = 1,
                   FieldId = 11,
                   IsEnable = true
               }
           );
            modelBuilder.Entity<WizardCustomerMap>().HasData(
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 1,
                  CustomerId = 1,
                  WizardId = 1,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 2,
                  CustomerId = 1,
                  WizardId = 2,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 3,
                  CustomerId = 1,
                  WizardId = 3,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 4,
                  CustomerId = 1,
                  WizardId = 4,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 5,
                  CustomerId = 1,
                  WizardId = 5,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 6,
                  CustomerId = 1,
                  WizardId = 6,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 7,
                  CustomerId = 1,
                  WizardId = 7,
                  IsEnable = true
              },
              new WizardCustomerMap
              {
                  WizardCustomerMapId = 8,
                  CustomerId = 1,
                  WizardId = 8,
                  IsEnable = true
              }
          );
        }
    }
}
