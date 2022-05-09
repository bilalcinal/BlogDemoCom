using System;
using System.Collections.Generic;
using System.Text;
using Data.Base;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Data.Domain
{
    public class APIResult 
    {
        public Meta meta { get; set; }

        [JsonProperty("data")]
        public IList<DataResults> data { get; set; }
    }
    
    
}