using MimeKit;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MailKit.Net.Smtp;
using MRODBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MimeKit.Utils;

namespace MROWebApi.Services
{
    public static class Utilities
    {
        public static string ReplaceWizardWitheXpress(string input)
        {
            return input.Replace("Wizard", "eXpress");
        }
        public static string ReplaceeXpressWithWizard(string input)
        {
            return input.Replace("eXpress", "Wizard");
        }
        public async static Task<bool> SendEmail(SendEmail sendEmail)
        {
            FacilitiesRepository fRep = new FacilitiesRepository(sendEmail.info);
            Facilities dbFacility = await fRep.Select(sendEmail.nFacilityID);
            FacilityLocationsRepository lRep = new FacilityLocationsRepository(sendEmail.info);
            FacilityLocations dbLocation = await lRep.Select(sendEmail.nLocationID);

            #region Decrypt SMTP Password
            MROLogger password = new MROLogger(sendEmail.info);
            dbFacility.sSMTPPassword = password.DecryptString(dbFacility.sSMTPPassword);
            #endregion

            //From 
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Admin " + dbFacility.sFacilityName, dbFacility.sOutboundEmail);
            message.From.Add(from);

            //To           
            MailboxAddress to = new MailboxAddress(sendEmail.sToMailName, sendEmail.sToMailAddress);
            message.To.Add(to);

            //Subject
            message.Subject = sendEmail.sSubject;

            //Body
            BodyBuilder bodyBuilder = new BodyBuilder();
            dbLocation.sConfigLogoData = Regex.Replace(dbLocation.sConfigLogoData, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            byte[] locationLogo = Convert.FromBase64String(dbLocation.sConfigLogoData);
            var image = bodyBuilder.LinkedResources.Add("locationlogo", locationLogo);
            image.ContentId = MimeUtils.GenerateMessageId();
            string htmlText = string.Format(@"<div style='border:1px solid black;padding: 25px;'>
                <img width=""300"" height=""150"" src=""cid:{0}""><br/><br/>
                <div style='margin-left: 25px;margin-right: 25px;text-align:justify;text-justify: inter-word;'>
                <p>Thank you!</p>
                {1}
                </div>
                <div style='margin: 20px;'>
                <p>
                <h4  style='text-align:center;'>CONFIDENTIALITY NOTICE</p>
                </h4>
                <p style='text-align:justify;text-justify: inter-word;'>This communication is confidential property and privileged communication of the sender intended only
                for the person/entity to which it is addressed. If you are not the intended recipient, you are notified
                that any use, review, disclosure, distribution, or taking of any other action relevant to the
                contents of this message is strictly prohibited. If this message was received in error, please notify
                privacy@mrocorp.com immediately.</p>
                </div>
                </div>", image.ContentId, sendEmail.sBody);
            bodyBuilder.HtmlBody = htmlText;
            message.Body = bodyBuilder.ToMessageBody();
            //GET Port number
            //Make SSL true
            try
            {

                SmtpClient client = new SmtpClient();
                client.Connect(dbFacility.sSMTPUrl, 25, false);
                if (dbFacility.sSMTPUrl.Contains("protection"))
                {
                    client.Capabilities &= ~SmtpCapabilities.Pipelining;
                }
                else
                {
                    client.Authenticate(dbFacility.sSMTPUsername, dbFacility.sSMTPPassword);
                }
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
