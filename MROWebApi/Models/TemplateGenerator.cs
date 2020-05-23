using MROWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard_Demo.Models
{
    public class TemplateGenerator
    {
        public static string GetHtmlTemplate(Patient PassedPatient)
        {
            //var commPrefString = String.Join(", ", PassedPatient.PrefComm);
            //Not Compulsion Items
            var PatDec = PassedPatient.IsPatientDeceased  ? "Yes" : "No";
            var middleInitials = string.IsNullOrEmpty(PassedPatient.MInitial) ? "" : PassedPatient.MInitial;
            var NotP = PassedPatient.NotPatient? "No" : "Yes";
            var RName = string.IsNullOrEmpty(PassedPatient.RName) ? "Not Applicable" : PassedPatient.RName;
            var RelToP = string.IsNullOrEmpty(PassedPatient.RelationToPatient) ? "Not Applicable" : PassedPatient.RelationToPatient;
            var FormRequest = PassedPatient.ConfirmReport? "Opted to be mailed to registered Email ID" : "Not Opted";
            var sb = new StringBuilder();
            sb.Append(@"<html>
						<head></head>
						<body>");
            sb.AppendFormat(@"<br />
                            <h4>Generated Report for Patient: {0}</h4>
                            <br />
							<table class='minimalistBlack'>
                            <thead>
                              <tr>
                              <th>Form Field</th>
                              <th>Value Provided</th>
                              </tr>
                              </thead>
								<tbody>
                                <tr>
                              <td>Selected Location</td>
                              <td>{1}</td>
                              </tr>
                              <tr>
                              <td>Form Filled By Patient ?</td>
                              <td>{2}</td>
                              </tr>
                              <tr>
                              <td>Relative Name</td>
                              <td>{3}</td>
                              </tr>
                              <tr>
                              <td>Relation With Patient</td>
                              <td>{4}</td>
                              </tr>
                              <tr>
                              <td>Email ID</td>
                              <td>{5}</td>
                              </tr>
                              <tr>
                              <td>Form Request ?</td>
                              <td>{6}</td>
                              </tr>
                              <tr>
                              <td>First Name</td>
                              <td>{7}</td>
                              </tr>
                              <tr>
                              <td>Middle Initials</td>
                              <td>{8}</td>
                              </tr>
                              <tr>
                              <td>Last Name</td>
                              <td>{9}</td>
                              </tr>
                              <tr>
                              <td>Is Patient Deceased ?</td>
                              <td>{10}</td>
                              </tr>
                              <tr>
                              <td>Postal Code</td>
                              <td>{11}</td>
                              </tr>
                              <tr>
                              <td>Street Area</td>
                              <td>{12}</td>
                              </tr><tr>
                              <td>Date of Birth</td>
                              <td>{13}</td>
                              </tr>
                                <tr>
                              <td valign='top'>Patient's Signature</td>
                               <td height='200'><img style='height:150px;width:200px' src='{14}'></td>
                              </tr>
                                ",
                                  PassedPatient.FName + middleInitials + PassedPatient.LName, PassedPatient.SelectedLocation,
                                  NotP, RName, RelToP,
                                  PassedPatient.EmailID, FormRequest, PassedPatient.FName,
                                  middleInitials, PassedPatient.LName, PatDec, PassedPatient.PostalCode, PassedPatient.StreetArea, PassedPatient.BDay, PassedPatient.imgData
                                  );
            sb.Append(@"
                                </tbody>
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
