using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Sample_Data
{
    public class CarpoolDtoWithUserInformationSampleProvider : IExamplesProvider<CarpoolDtoWithUserInformation>
    {
        public CarpoolDtoWithUserInformation GetExamples()
        {
            return new CarpoolDtoWithUserInformation
            {
                CarpoolId = 0,
                CarDesignation = "BMW",
                FreeSeat = 4,
                StartPoint = "Schweinberg",
                EndPoint = "Weikersheim",
                DepartureTime = DateTime.Now,
                Drivers = new UserInfoDto
                {
                    Id = 0,
                    Name = "Marcello",
                    IsDriver = true,
                },
                Passengers = new List<UserInfoDto>()
                {
                    new UserInfoDto
                    {
                        Id = 1,
                        Name="Jonas",
                        IsDriver=false,
                    },
                    new UserInfoDto
                    {
                        Id = 1,
                        Name="Lukas",
                        IsDriver=false,
                    }
                }
            };
        }
    }
}