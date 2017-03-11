using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprilisJam.Data
{
    public class EmailSettings
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSendingEnabled { get; set; }
    }
}
