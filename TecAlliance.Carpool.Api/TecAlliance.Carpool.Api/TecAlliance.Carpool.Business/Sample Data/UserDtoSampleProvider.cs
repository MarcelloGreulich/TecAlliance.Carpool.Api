using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Models;

namespace TecAlliance.Carpool.Business.Sample_Data
{

    public class UserDtoSampleProvider : IExamplesProvider<UserDto>
    {
        public UserDto GetExamples()
        {
            return new UserDto()
            {
                Id = 0,
                Name = "Marcello",
                Nachname = "Greulich",
                Anmeldename = "MAGR",
                Passwort = "123",
                Gender = "Männlich",
                Alter = 12
            };
        }
    }
}