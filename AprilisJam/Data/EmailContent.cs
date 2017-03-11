using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprilisJam.Data
{
    public class EmailContent
    {
        public string Title { get; set; }
        public int MemberThreshold { get; set; }
        public string ContentIfOver { get; set; }
        public string ContentIfUnder { get; set; }
    }
}
