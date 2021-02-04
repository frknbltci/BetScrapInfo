using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Models
{
    public class UrlViewModel
    {


        public int Id { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }

        public List<Url> UrlList { get; set; }

    }
}
