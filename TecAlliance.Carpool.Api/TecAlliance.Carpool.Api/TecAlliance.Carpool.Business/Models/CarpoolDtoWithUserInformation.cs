using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Data.Model;

namespace TecAlliance.Carpool.Business.Models
{
    public class CarpoolDtoWithUserInformation
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

        //Contains the Driver of the Carpool    
        public UserInfoDto Drivers { get; set; }
        //Contains a list of Pessangers
        public List<UserInfoDto> Passengers { get; set; }

    }
}
