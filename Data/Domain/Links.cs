using System;
using System.Collections.Generic;
using System.Text;
using Data.Base;
using System.Web.Mvc;
namespace Data.Domain
{
    public class Links
    {
        public IList<string> previous { get; set; }
        public string current { get; set; }
        public string next { get; set; }
    }
}