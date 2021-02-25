using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MROWebApi.Services
{
    public static class Utilities
    {
        public static string ReplaceWizardWithExpress(string input)
        {
            return input.Replace("Wizard", "Express");
        }
        public static string ReplaceExpressWithWizard(string input)
        {
            return input.Replace("Express", "Wizard");
        }
    }
}
