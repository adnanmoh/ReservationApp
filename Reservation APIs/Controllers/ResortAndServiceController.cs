using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;
using Reservation_APIs.Repositories;

namespace Reservation_APIs.Controllers
{
     
    public class ResortAndServiceController : BaseController
    {


        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortAndServiceDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserResortsAndServicess(int resortID)
        {
            try
            {
                var resortsServices = await RepositoryManager.ResortAndServiceRepository.GetAll(c => c.ResortId == resortID);

                if (resortsServices == null || !resortsServices.Any())
                {
                    return NotFound("No serveces found for the restor.");
                }

                var resortsServicesDTO = Mapper.Map<List<ResortAndServiceDTO>>(resortsServices);

                return Ok(resortsServicesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resorts serveces: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortAndServiceDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddResortAndService([FromBody] ResortAndServiceDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<ResortAndService>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ResortAndServiceRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortAndServiceRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding resortAndService. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new resortAndService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{ResortID}/{ServiceID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteResortAndService(int ResortID,int ServiceID)
        {
            try
            {
                var existingObj = await RepositoryManager.ResortAndServiceRepository.GetById(new object[] { ResortID, ServiceID });
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ResortAndServiceRepository.Remove(existingObj);
                var res = await RepositoryManager.ResortAndServiceRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting ResortAndService. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting ResortAndService: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }

   
}
