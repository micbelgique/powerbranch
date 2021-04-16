using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Model
{
    public class Log
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public string Url { get; set; }
        public DateTime Time { get; set; }
    }
}
