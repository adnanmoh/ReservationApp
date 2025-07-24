using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;

namespace Reservation_APIs.Controllers
{
    public class TypesOfServiceController : BaseController
    {
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TypesOfServiceDTO>))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTypesOfServices()
        {
            try
            {
                var typesOfServices = await RepositoryManager.TypesOfServiceRepository.GetAll();

                if (typesOfServices == null || !typesOfServices.Any())
                {
                    return NotFound();
                }

                var typesOfServicesDTO = Mapper.Map<List<TypesOfServiceDTO>>(typesOfServices);

                return Ok(typesOfServicesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving typesOfServices: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
