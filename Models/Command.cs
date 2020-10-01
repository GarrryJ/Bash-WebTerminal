using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShell.Models
{
    public class Command
    {
        public long Id { get; set; }
        public long cmdId { get; set; }
        public string Line { get; set; }
    }
}