using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Data.Model
{
    public class UserInfo
    {
        //Contains Id of the UserDto
        public int Id { get; set; }
        //Contains Name of the UserDto
        public string? Name { get; set; }

        public bool IsDriver { get; set; } 
    }
}
