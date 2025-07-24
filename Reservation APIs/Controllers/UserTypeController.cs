using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;

namespace Reservation_APIs.Controllers
{
    public class UserTypeController : BaseController
    {
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserTypeDTO>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserTypes()
        {
            try
            {
                var userTypes = await RepositoryManager.UserTypeRepository.GetAll();

                if (userTypes == null || !userTypes.Any())
                {
                    return NotFound();
                }

                var userTypesDTO = Mapper.Map<List<UserTypeDTO>>(userTypes);

                return Ok(userTypesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving userTypes: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
