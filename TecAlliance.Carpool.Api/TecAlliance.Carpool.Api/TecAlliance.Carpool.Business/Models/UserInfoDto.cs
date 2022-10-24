using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class UserDto
    {
        //Contains Id of the UserDto
        public int Id { get; set; }
        //Contains Name of the UserDto
        public string? Name { get; set; }
        //Contains last Name of the UserDto
        public string? Nachname { get; set; }
        //Contains login Name of the UserDto
        public string? Anmeldename { get; set; }
        //Contains passwort of the UserDto
        public string? Passwort { get; set; }
        //Contains gender of the UserDto
        public string? Gender { get; set; }
        //Contains age of the UserDto
        public int Alter { get; set; }
    }
}
