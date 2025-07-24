using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ReserveController : BaseController
    {
        [HttpGet("[action]/{userID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReservationsdetailsDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserReservations(int userID)
        {
            try
            {
                // جلب الحجوزات
                var reserves = await RepositoryManager.ReserveRepository.GetAll(c => c.UserId == userID);

                if (reserves == null || !reserves.Any())
                {
                    return NotFound("No reservations found for the user.");
                }

                // جلب المنتجعات لكل حجز
                var reservationsdetailsDTOList = new List<ReservationsdetailsDTO>();

                foreach (var reserve in reserves)
                {
                    var resort = await RepositoryManager.ResortRepository.Find(r => r.ResortId == reserve.ResortId);

                    if (resort != null)
                    {
                        var reservationsdetailsDTO = new ReservationsdetailsDTO
                        {
                            ReserveId = reserve.ReserveId,
                            ReserveDate = reserve.ReserveDate,
                            DepartureDate = reserve.DepartureDate,
                            IsApproved = reserve.IsApproved,
                            IsRejected = reserve.IsRejected,
                            IsReady = reserve.IsReady,
                            Reason = reserve.Reason,
                            ResortId = reserve.ResortId,
                            UserId = reserve.UserId,
                            ResortName = resort.Name  
                        };

                        reservationsdetailsDTOList.Add(reservationsdetailsDTO);
                    }
                }

                return Ok(reservationsdetailsDTOList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving reservations: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }





        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DateTime>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetResortDatesReservations(int resortID)
        {
            try
            {
                var reserves = await RepositoryManager.ReserveRepository.GetAll(c => c.ResortId == resortID && c.IsApproved == true);

                if (reserves == null || !reserves.Any())
                {
                    return NoContent();
                }

                var reserveDates = reserves.Select(r => r.DepartureDate).ToList();

                return Ok(reserveDates);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving reservations: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReserveDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetNotApprovedReservations()
        {
            try
            {
                var reserves = await RepositoryManager.ReserveRepository.GetAll(c => c.IsApproved == false && c.IsRejected == false);

                if (reserves == null || !reserves.Any())
                {
                    return NoContent();
                }

                var reservesDTO = Mapper.Map<List<ReserveDTO>>(reserves);

                return Ok(reservesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving reservations: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("[action]/{reserveID}")]
        [ProducesResponseType(200, Type = typeof(ReserveDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetReserveByID(int reserveID)
        {
            try
            {
                var isExist = await RepositoryManager.ReserveRepository.ObjExists(reserveID);
                if (!isExist)
                {
                    return NotFound();
                }
                var reserve = await RepositoryManager.ReserveRepository.GetById(reserveID);

                var reserveDTO = Mapper.Map<ReserveDTO>(reserve);
                return Ok(reserveDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Reserve: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddReserve([FromBody] ReserveDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<Reserve>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ReserveRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ReserveRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding reservation. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new reservation: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpPut("[action]/{reserveID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditReserve(int reserveID, [FromBody] ReserveDTO objDTO)
        {
            try
            {
                if (reserveID != objDTO.ReserveId)
                {
                    return BadRequest("Invalid Reserve ID.");
                }

                var existingObj = await RepositoryManager.ReserveRepository.GetById(reserveID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<Reserve>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid Reserve data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ReserveRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ReserveRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing reservation. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing reservation: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{reserveID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteReserve(int reserveID)
        {
            try
            {
                var existingObj = await RepositoryManager.ReserveRepository.GetById(reserveID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ReserveRepository.Remove(existingObj);
                var res = await RepositoryManager.ReserveRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting reservation. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting reservation: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
