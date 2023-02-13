using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsHotRods.Models
{
    public class UserOnly
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string UserType { get; set; }
        public string Story { get; set; }
        public string SoThat { get; set; }
        public string PostedBy { get; set; }
    }
}
