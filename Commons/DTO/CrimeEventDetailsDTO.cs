using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.DTO
{
    public class CrimeEventDetailsDTO
    {
        public string CrimeEventId { get; set; }
        public DateTime DateTime { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ReportingPersonalEmail { get; set; }



    }
}
