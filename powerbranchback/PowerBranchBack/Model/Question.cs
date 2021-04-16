using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerBranchBack.Model
{
    public class Question
    {
        public int Id { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public int RightAnswerId { get; set; }
        public Event EventLinked { get; set; }
    }
}
