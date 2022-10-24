using Microsoft.AspNetCore.Mvc;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Business.Services;

namespace TecAlliance.Carpool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Glogal
        UserBusinessServices businessServices;

        //Constructor
        public UserController()
        {
            businessServices = new UserBusinessServices();
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<UserDto> PostUserDto(UserDto user)
        {
            //Add UserDto in businessServices
            businessServices.AddUser(user);

            return Created($"api/User/{user.Id}", user);
        }
        //Get: api/User
        [HttpGet]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            return businessServices.GetAllUsers();
        }
        //Get: api/User/Id
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserdtoById(int id)
        {
            bool b = businessServices.FindUserDtoId(id);
            if (b)
            {
                UserDto user = businessServices.GetUserdtoById(id);
                if (user==null)
                {
                    return NotFound();
                }
                else
                {
                    return user;
                }
               
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/User/Id
        [HttpDelete("{id}")]
        public ActionResult<UserDto> DeleteUserById(int id)
        {
            bool b = businessServices.FindUserDtoId(id);
            if (b)
            {
                return businessServices.RemoveUserById(id);
            }
            else
            {
                return NotFound();
            }
        }
        // DELETE: api/User/
        [HttpDelete]
        public ActionResult<List<UserDto>> DeleteAllUsers()
        {
            businessServices.RemoveAllUser();
            return NoContent();
        }
        // PUT: api/User/Id
        [HttpPut ("id")]
        public ActionResult<UserDto> ReplaceUserById(int id, UserDto user)
        {
            bool b = businessServices.FindUserDtoId(id);
            if (b)
            {
                return businessServices.ReplaceUserById(id, user);
            }
            else
            {
                return NotFound();
            }
        } 
    }

}
