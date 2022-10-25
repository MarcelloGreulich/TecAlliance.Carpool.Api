using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Sample_Data
{
    public class ListUserDtoSampleProvider : IExamplesProvider<List<UserDto>>
    {
        public List<UserDto> GetExamples()
        {
            return new List<UserDto>()
            {
                new UserDto
                {
                    Id = 0,
                    Name = "Marcello",
                    Nachname = "Greulich",
                    Anmeldename = "MAGR",
                    Passwort = "123",
                    Gender = "Männlich",
                    Alter = 12
                },
                new UserDto
                {
                    Id = 1,
                    Name = "Jonas",
                    Nachname = "bundschuh",
                    Anmeldename = "JOBU",
                    Passwort = "123",
                    Gender = "Männlich",
                    Alter = 2
                },
            };
        }
    }
}
