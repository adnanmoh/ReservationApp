using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;

namespace Reservation_APIs.Controllers
{
    public class ResortTypeController : BaseController
    {
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortTypeDTO>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetResortTypes()
        {
            try
            {
                var resortTypes = await RepositoryManager.ResortTypeRepository.GetAll();

                if (resortTypes == null || !resortTypes.Any())
                {
                    return NotFound();
                }

                var resortTypesDTO = Mapper.Map<List<ResortTypeDTO>>(resortTypes);

                return Ok(resortTypesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resortTypes: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
