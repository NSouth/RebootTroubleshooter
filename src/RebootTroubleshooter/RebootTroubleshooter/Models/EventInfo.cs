using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebootTroubleshooter.Models
{
    public class EventInfo
    {
        public long EventId { get; set; } // The value the user would see in the event log
        public string EventCodeHumanized { get; set; } = string.Empty;
    }
}
