using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Shared
{
    public static class StringExtension
    {
        public static string GetSPCmd(this string spName)
        {
            return "exec " + spName;
        }
    }
}
