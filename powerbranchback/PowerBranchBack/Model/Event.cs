using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Model
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
