using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ResortsPhotoController : BaseController
    {


        [HttpGet("[action]/{photoID}")]
        [ProducesResponseType(200, Type = typeof(ResortsPhotoDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetResortsPhotoByID(int photoID)
        {
            try
            {
                var isExist = await RepositoryManager.ResortsPhotoRepository.ObjExists(photoID);
                if (!isExist)
                {
                    return NotFound();
                }
                var resortsPhoto = await RepositoryManager.ResortsPhotoRepository.GetById(photoID);

                var resortsPhotoDTO = Mapper.Map<ResortsPhotoDTO>(resortsPhoto);
                return Ok(resortsPhotoDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Resorts Photo: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ResortsPhotoDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserResortsPhotos(int resortID)
        {
            try
            {
                var resortsPhotos = await RepositoryManager.ResortsPhotoRepository.GetAll(c => c.ResortId == resortID);

                if (resortsPhotos == null || !resortsPhotos.Any())
                {
                    return NotFound("No resorts Photos found for the user.");
                }

                var resortsPhotosDTO = Mapper.Map<List<ResortsPhotoDTO>>(resortsPhotos);

                return Ok(resortsPhotosDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resortsPhotos: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]/{resortID}")]
        [ProducesResponseType(200, Type = typeof(ResortsPhotoDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserResortsMainPhoto(int resortID)
        {
            try
            {
                var resortsPhotos = await RepositoryManager.ResortsPhotoRepository.Find(c => c.ResortId == resortID && c.IsMain == true);

                if (resortsPhotos == null )
                {
                    return NotFound("No resorts Photos found for the user.");
                }

                var resortsPhotosDTO = Mapper.Map<ResortsPhotoDTO>(resortsPhotos);

                return Ok(resortsPhotosDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving resortsPhotos: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }





        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddResortsPhoto([FromBody] ResortsPhotoDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<ResortsPhoto>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ResortsPhotoRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortsPhotoRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding resorts Photo. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new resorts Photo: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{photoID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditResortsPhoto(int photoID, [FromBody] ResortsPhotoDTO objDTO)
        {
            try
            {
                if (photoID != objDTO.PhotoId)
                {
                    return BadRequest("Invalid ResortsPhoto ID.");
                }

                var existingObj = await RepositoryManager.ResortsPhotoRepository.GetById(photoID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<ResortsPhoto>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid ResortsPhoto data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ResortsPhotoRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ResortsPhotoRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing Resorts Photo. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing Resorts Photo: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{photoID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteResortsPhoto(int photoID)
        {
            try
            {
                var existingObj = await RepositoryManager.ResortsPhotoRepository.GetById(photoID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ResortsPhotoRepository.Remove(existingObj);
                var res = await RepositoryManager.ResortsPhotoRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting Resorts Photo. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting Resorts Photo: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
