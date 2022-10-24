using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Business.Services;
using TecAlliance.Carpool.Data.Model;
using TecAlliance.Carpool.Data.Models;

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
        /// Creates CarpoolDto.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="carpool"></param>
        /// <param name="isDriver"></param>
        /// <returns>A newly created Carpooldto</returns>
        /// <remarks>
        /// Sample request:
        ///  POST /Carpool
        ///     {
        ///        		"carpoolId": 0,
        ///             "carDesignation": "BMW",
        ///             "freeSeat": 4,
        ///             "startPoint": "Schweinberg",
        ///             "endPoint": "Weikersheim",
        ///             "depatureTime": "2022-10-24T11:48:38.704Z"
        ///     }
        ///
        /// </remarks>
        ///  <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        //       public ActionResult<CarpoolDto> PostCarpool(int userid, bool isDriver, CarpoolDto carpool)
        public ActionResult<CarpoolDto> PostCarpool(CarpoolDto carpool)

        {
            carpoolBusinessServices.PostCarpool(0, carpool, false);
            return Created($"api/Carpool/{carpool.CarpoolId}", carpool); ;
        }
        [HttpPost("another")]
        public ActionResult<CarpoolDtoWithUserInformation> PostFullCarpool(CarpoolDtoWithUserInformation carpool)
        {
            carpoolBusinessServices.AddCarpool(carpool);

            return Created($"api/User/{carpool.CarpoolId}", carpool);
        }
        //GET: api/Carpool
        [HttpGet]
        public ActionResult<List<CarpoolDtoWithUserInformation>> GetAllCarpools()
        {
            return carpoolBusinessServices.GetAllCarpools();
        }
        [HttpGet("/CarpoolID")]
        public ActionResult<CarpoolDtoWithUserInformation> GetCarpoolById(int id)
        {
            return carpoolBusinessServices.GetCarpoolById(id);
        }
        //DELETE: api/Carpool
        [HttpDelete]
        public ActionResult<List<UserDto>> DeleteAllCarpools()
        {
            carpoolBusinessServices.DeleteAllCarpools();
            return NoContent();
        }
        //DELETE: api/Carpool/CarpoolId
        [HttpDelete("{carpoolId}")]
        public ActionResult<CarpoolModel> DeleteCarpoolsById(int carpoolId)
        {
            carpoolBusinessServices.DeleteCarpoolsId(carpoolId);
            return NoContent();
        }
        //PUT: api/Carpool/carpoolId/userId
        [HttpPut("{carpoolId}/{userId}")]
        public ActionResult<CarpoolModel> LeaveCarpool(int carpoolId, int userId)
        {
            return carpoolBusinessServices.LeaveCarpool(carpoolId, userId);
        }
    }
}
