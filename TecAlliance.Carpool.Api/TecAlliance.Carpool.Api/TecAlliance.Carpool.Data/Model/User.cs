using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Models
{
    public class User
    {
        //Contains Id of the User
        public int Id { get; set; }
        //Contains Name of the User
        public string? Name { get; set; }
        //Contains last Name of the User
        public string? Nachname { get; set; }
        //Contains login Name of the User
        public string? Anmeldename { get; set; }
        //Contains passwort of the User
        public string? Passwort { get; set; }
        //Contains gender of the User
        public string? Gender { get; set; }
        //Contains age of the User
        public int Alter { get; set; }

    }
}
