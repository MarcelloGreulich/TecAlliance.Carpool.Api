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

        /// <summary>
        /// Creates User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UserDto</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> PostUserDto(UserDto user)
        {
            //Add UserDto in businessServices
            businessServices.AddUser(user);

            return Created($"api/User/{user.Id}", user);
        }
        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <returns>List</returns>
        /// <response code="200">Returns all Users</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            return businessServices.GetAllUsers();
        }
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDto</returns>
        /// <response code="200">Returns User</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Delte User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserDto</returns>
        /// <response code="204">User sucsessfully deleted</response>
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
        /// <summary>
        /// Deletes all Users 
        /// </summary>
        /// <returns>List</returns>
        /// <response code="204">Users sucsessfully deleted</response>
        [HttpDelete]
        public ActionResult<List<UserDto>> DeleteAllUsers()
        {
            businessServices.RemoveAllUser();
            return NoContent();
        }
        /// <summary>
        /// Change User Information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Returns all Users</response>
        /// <response code="400">If the item is null</response>
        [HttpPut ("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
