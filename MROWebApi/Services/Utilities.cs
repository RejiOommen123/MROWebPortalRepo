using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
