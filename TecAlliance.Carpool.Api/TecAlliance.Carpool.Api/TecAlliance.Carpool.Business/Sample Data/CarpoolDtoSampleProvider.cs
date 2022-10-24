using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Sample_Data
{
    public class CarpoolDtoSampleProvider : IExamplesProvider<CarpoolDto>
    {
        public CarpoolDto GetExamples()
        {
            return new CarpoolDto
            {
                CarpoolId = 0,
                CarDesignation = "BMW",
                FreeSeat = 5,
                StartPoint = "Schweinberg",
                EndPoint = "Weikersheim",
                DepartureTime = System.DateTime.Now,
            };  
        }
    }
}
