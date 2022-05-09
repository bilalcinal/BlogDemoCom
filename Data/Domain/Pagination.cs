using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Domain
{
    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
        public Links links { get; set; }
    }
}
