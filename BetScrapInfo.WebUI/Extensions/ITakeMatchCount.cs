using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Extensions
{
   public interface ITakeMatchCount
    {

        int GetCount(string Url);
        
    }
}
