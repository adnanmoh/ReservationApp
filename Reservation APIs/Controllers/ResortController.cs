using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ResortController : BaseController
    {

        [HttpGet("[action]/{userID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserResorts(int userID)
        {
            try
            {
                var resorts = await RepositoryManager.ResortRepository.GetAll(c => c.UserId == userID);

                if (resorts == null || !resorts.Any())
                {
                    return NoContent();
                }

                var resortsDTO = Mapper.Map<List<ResortDTO>>(resorts);

                return Ok(resortsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resorts: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet("[action]/{resortTypeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReserveDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllResortForCustomer(int resortTypeID)
        {
            try
            {
                var resorts = await RepositoryManager.ResortRepository.GetAll(c => c.IsActive == true && c.IsApprovedAdd == true && c.IsApprovedEdit == true && c.ResortTypeId == resortTypeID);

                if (resorts == null || !resorts.Any())
                {
                    return Ok();
                }

                var resortsDTO = Mapper.Map<List<ResortDTO>>(resorts);

                return Ok(resortsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resorts: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReserveDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetNotApprovedResort()
        {
            try
            {
                var resorts = await RepositoryManager.ResortRepository.GetAll(c =>  c.IsApprovedAdd == false || c.IsApprovedEdit == false);

                if (resorts == null || !resorts.Any())
                {
                    return NoContent();
                }

                var resortsDTO = Mapper.Map<List<ResortDTO>>(resorts);

                return Ok(resortsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resorts: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(ResortDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetResortByID(int resortID)
        {
            try
            {
                var isExist = await RepositoryManager.ResortRepository.ObjExists(resortID);
                if (!isExist)
                {
                    return NotFound();
                }
                var resort = await RepositoryManager.ResortRepository.GetById(resortID);

                var resortDTO = Mapper.Map<ResortDTO>(resort);
                return Ok(resortDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Resort: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddResort([FromBody] ResortDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<Resort>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ResortRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortRepository.SaveChangesAsync();
                   var added =  await RepositoryManager.ResortRepository.Find(u => u.Name == objDTO.Name && u.Address == objDTO.Address);
                    return Ok(added.ResortId);
                }

                return StatusCode(500, "There was a problem adding resort. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new resort: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{resortID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditResort(int resortID, [FromBody] ResortDTO objDTO)
        {
            try
            {
                if (resortID != objDTO.ResortId)
                {
                    return BadRequest("Invalid Resort ID.");
                }

                var existingObj = await RepositoryManager.ResortRepository.GetById(resortID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<Resort>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid Resort data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ResortRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing Resort. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing Resort: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{resortID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ChangeResortStatus(int resortID)
        {
            try
            {
                var existingObj = await RepositoryManager.ResortRepository.GetById(resortID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                existingObj.IsActive = !existingObj.IsActive; // تغيير حالة المنتجع

                var res = RepositoryManager.ResortRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem changing Resort status. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while changing Resort status: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{resortID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteResort(int resortID)
        {
            try
            {
                var existingObj = await RepositoryManager.ResortRepository.GetById(resortID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ResortRepository.Remove(existingObj);
                var res = await RepositoryManager.ResortRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting Resort. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting Resort: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
