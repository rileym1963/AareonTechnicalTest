using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest
{
    public class TicketDetail
    {
        public Models.Ticket Ticket { get; set; }
        public Models.Person Person { get; set; }
        public IEnumerable<Models.Note> Notes { get; set; }
    }
}
