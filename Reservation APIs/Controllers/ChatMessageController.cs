using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Reservation_APIs.DTOs;
using Reservation_APIs.Hubs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ChatMessageController : BaseController
    {

        [HttpGet("[action]/{chatID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ChatsMessageDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllMessageByChatID(int chatID)
        {
            try
            {
                var chatMessages = await RepositoryManager.ChatsMessageRepository.GetAll(c => c.ChatId == chatID);

                if (chatMessages == null || !chatMessages.Any())
                {
                    return NotFound("No chatMessages found for the user.");
                }

                var chatMessagesDTO = Mapper.Map<List<ChatsMessageDTO>>(chatMessages);

                return Ok(chatMessagesDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving chatMessages: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddChatsMessage([FromBody] ChatsMessageDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<ChatsMessage>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ChatsMessageRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ChatsMessageRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding chatsMessage. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new chatsMessage: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{chatsMessageID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditChatsMessage(int chatsMessageID, [FromBody] ChatsMessageDTO objDTO)
        {
            try
            {
                if (chatsMessageID != objDTO.Id)
                {
                    return BadRequest("Invalid ChatsMessage ID.");
                }

                var existingObj = await RepositoryManager.ChatsMessageRepository.GetById(chatsMessageID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<ChatsMessage>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid ChatsMessage data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ChatsMessageRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ChatsMessageRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing ChatsMessage. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing ChatsMessage: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{chatsMessageID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteChatsMessage(int chatsMessageID)
        {
            try
            {
                var existingObj = await RepositoryManager.ChatsMessageRepository.GetById(chatsMessageID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ChatsMessageRepository.Remove(existingObj);
                var res = await RepositoryManager.ChatsMessageRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting ChatsMessage. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting ChatsMessage: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
