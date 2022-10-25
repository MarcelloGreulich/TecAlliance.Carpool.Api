using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Sample_Data
{
    public class ListCarpoolDtoWithUserInformationsampleProvider : IExamplesProvider<List<CarpoolDtoWithUserInformation>>
    {
        public List<CarpoolDtoWithUserInformation> GetExamples()
        {
            return new List<CarpoolDtoWithUserInformation>
            {
                new CarpoolDtoWithUserInformation
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
                },
                                new CarpoolDtoWithUserInformation
                {
                    CarpoolId = 0,
                    CarDesignation = "BMW",
                    FreeSeat = 4,
                    StartPoint = "Weikersheim",
                    EndPoint = "Schweinberg",
                    DepartureTime = DateTime.Now,
                    Drivers = new UserInfoDto
                    {
                        Id = 1,
                        Name = "Lukas",
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
                            Id = 0,
                            Name="",
                            IsDriver=false,
                        }
                    }
                },
            };
        }
    }
}
