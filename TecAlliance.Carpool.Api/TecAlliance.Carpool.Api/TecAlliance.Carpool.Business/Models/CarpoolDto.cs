using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class CarpoolDto
    {
        //Contains Id of Carpool
        public int CarpoolId { get; set; }
        //Contains the Designation of the Drivers car
        public string? CarDesignation { get; set; }
        //Contains the number of free Steats in car
        public int FreeSeat { get; set; }
        //Conains the Start point of the Carpool
        public string? StartPoint { get; set; }
        //Conains the End point of the Carpool
        public string? EndPoint { get; set; }
        // Contains Departure Time of The Carpool
        public DateTime DepartureTime { get; set; }
    }
}
