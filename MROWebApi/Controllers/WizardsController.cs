using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MRODBL.BaseClasses;
using MRODBL.BaseClassRepositories;
using MRODBL.Entities;
using WebSupergoo.ABCpdf11.Elements;

namespace MROWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class WizardsController : ControllerBase
    {
        private readonly DBConnectionInfo _info;

        #region Wizards Constructor
        public WizardsController(DBConnectionInfo info)
        {
            _info = info;
        }
        #endregion  

        #region Get Wizard Config
        [HttpGet("GetWizardConfig/{nFacilityID}")]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<object> GetWizardConfigurationAsync(int nFacilityID)
        {
            try
            {
                FieldsRepository fieldsRepository = new FieldsRepository(_info);
                object Wizard_Config = await  fieldsRepository.GetWizardConfigurationAsync(nFacilityID);
                return Wizard_Config;

                //var oWizardConfigFields = Function(wizard_config.ofield);
                //return OptimizeObject(unoptimized_wizard_config);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex;
            }
        }
        #endregion

        #region Send Only Required Values
        private object OptimizeObject(object unoptimized_wizard_config) {

            object wizard_config = null;

            //var oFields = unoptimized_wizard_config.

            return wizard_config;
        }
        #endregion
    }
}
