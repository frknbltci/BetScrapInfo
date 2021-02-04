using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Extensions
{
    public static class StringOperations
    {

        public static string ReplaceSession(string strData)
        {
            if (strData==null)
            {
                return "";
            }
            else
            {
                return strData.Replace(@"""", "");
            }

        }


    }
}
