using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecAlliance.Carpool.Business.Models
{
    public class UserInfoDto
    {
        //Contains the id of the UserInoDto
        public int Id { get; set; }
        //Contains the Name of the UserInoDto
        public string Name { get; set; }
        //Contains bool that indicates if the user is Driver or not
        public bool IsDriver { get; set; }
    }
}
