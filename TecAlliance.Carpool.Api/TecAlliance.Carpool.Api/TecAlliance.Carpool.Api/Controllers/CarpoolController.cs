using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Business.Services;
using TecAlliance.Carpool.Data.Model;

namespace TecAlliance.Carpool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpoolController : ControllerBase
    {
        //Global
        CarpoolBusinessServices carpoolBusinessServices;

        //Construktor
        
        public CarpoolController()
        {
            carpoolBusinessServices = new CarpoolBusinessServices();
        }
        
        /// <summary>
        /// Creates CarpoolDto With user Id and bool.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carpool"></param>
        /// <param name="isDriver"></param>
        /// <returns>A newly created Carpooldto</returns>
        /// <remarks>
        /// </remarks>
        ///  <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("{userid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarpoolDto> PostCarpool(int userid, bool isDriver, CarpoolDto carpool)
        {
            carpoolBusinessServices.PostCarpool(userid, carpool, isDriver);
            return Created($"api/Carpool/{carpool.CarpoolId}", carpool); ;
        }
        /// <summary>
        /// Creates custom Carpool
        /// </summary>
        /// <param name="carpool"></param>
        /// <returns>CarpoolDtoWithUserInformation</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("another")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarpoolDtoWithUserInformation> PostFullCarpool(CarpoolDtoWithUserInformation carpool)
        {
            carpoolBusinessServices.AddCarpool(carpool);

            return Created($"api/User/{carpool.CarpoolId}", carpool);
        }
        /// <summary>
        /// Gets all Carpools
        /// </summary>
        /// <returns>CarpoolDtoWithUserInformation</returns>
        /// <response code="200">Returns all Carpools</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CarpoolDtoWithUserInformation>> GetAllCarpools()
        {
            return carpoolBusinessServices.GetAllCarpools();
        }
        /// <summary>
        /// Gets Carpool by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CarpoolDtoWithUserInformation</returns>
        /// <response code="200">Returns Carpool by Id</response>
        /// <response code="400">If the item is null</response>
        [HttpGet("/CarpoolID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarpoolDtoWithUserInformation> GetCarpoolById(int id)
        {
            return carpoolBusinessServices.GetCarpoolById(id);
        }
        /// <summary>
        /// Deletes all Carpools
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Carpools sucsessfully deleted</response>
        [HttpDelete]
        public ActionResult<List<CarpoolDtoWithUserInformation>> DeleteAllCarpools()
        {
            carpoolBusinessServices.DeleteAllCarpools();
            return NoContent();
        }
        /// <summary>
        /// Deletes all Carpools by Id
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <returns>CarpoolDtoWithUserInformation</returns>
        ///<response code="204">Carpools sucsessfully deleted by Id</response>
        [HttpDelete("{carpoolId}")]
        public ActionResult<CarpoolDtoWithUserInformation> DeleteCarpoolsById(int carpoolId)
        {
            carpoolBusinessServices.DeleteCarpoolsId(carpoolId);
            return NoContent();
        }
        /// <summary>
        /// Leave carpools
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        /// <returns>CarpoolDtoWithUserInformation</returns>
        /// <response code="200">Returns all Carpools</response>
        /// <response code="400">If the item is null</response>
        [HttpPut("{carpoolId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CarpoolDtoWithUserInformation> LeaveCarpool(int carpoolId, int userId)
        {
            return carpoolBusinessServices.LeaveCarpool(carpoolId, userId);
        }
    }
}
