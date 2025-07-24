using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ResortServiceController : BaseController
    {

        [HttpGet("[action]/{userID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable< ResortServiceDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllServiceForUser(int userID)
        {
            try
            {
                var resorts = await RepositoryManager.ResortServiceRepository.GetAll(c => c.UserId == userID);

                if (resorts == null || !resorts.Any())
                {
                    return NoContent();
                }

                var resortsDTO = Mapper.Map<List<ResortServiceDTO>>(resorts);

                return Ok(resortsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resort services: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortServiceDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllServiceForResort(int resortID)
        {
            try
            {
                var resortAndService = await RepositoryManager.ResortAndServiceRepository.GetAll(c => c.ResortId == resortID);
                var serviceIds = resortAndService.Select(rs => rs.ServiceId).ToList();
                var services = await RepositoryManager.ResortServiceRepository.GetAll(c => serviceIds.Contains(c.ServiceId));

                if (services == null || !services.Any())
                {
                    return NoContent();
                }

                var servicesDTO = Mapper.Map<List<ResortServiceDTO>>(services);

                return Ok(servicesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resort services: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("[action]/{serviceID}")]
        [ProducesResponseType(200, Type = typeof(ResortServiceDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetServiceById(int serviceID)
        {
            try
            {
                if (!await RepositoryManager.ResortServiceRepository.ObjExists(serviceID))
                {
                    return NotFound();
                }

                var resortService = await RepositoryManager.ResortServiceRepository.GetById(serviceID);

                var resortServiceDTO = Mapper.Map<ResortServiceDTO>(resortService);

                return Ok(resortServiceDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resortService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddResortService([FromBody] ResortServiceDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<ResortService>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ResortServiceRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortServiceRepository.SaveChangesAsync();
                    res = await RepositoryManager.ResortServiceRepository.Find(r => r.UserId == obj.UserId && r.ServiceTypeId == obj.ServiceTypeId&& r.Name == obj.Name );

                    return Ok(res?.ServiceId);
                }

                return StatusCode(500, "There was a problem adding resortService. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new resortService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpPut("[action]/{resortServiceID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditResortService(int resortServiceID, [FromBody] ResortServiceDTO objDTO)
        {
            try
            {
                if (resortServiceID != objDTO.ServiceId)
                {
                    return BadRequest("Invalid ResortService ID.");
                }

                var existingObj = await RepositoryManager.ResortServiceRepository.GetById(resortServiceID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<ResortService>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid ResortService data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ResortServiceRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortServiceRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing ResortService. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing ResortService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{resortServiceID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteResortService(int resortServiceID)
        {
            try
            {
                var existingObj = await RepositoryManager.ResortServiceRepository.GetById(resortServiceID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ResortServiceRepository.Remove(existingObj);
                var res = await RepositoryManager.ResortServiceRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting ResortService. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting ResortService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
