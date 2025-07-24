using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class ChatController : BaseController
    {


        [HttpGet("[action]/{userID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ChatDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserChats(int userID)
        {
            try
            {
                var chats = await RepositoryManager.ChatRepository.GetAll(c => c.SenderId == userID || c.ReceiverId == userID);

                if (chats == null || !chats.Any())
                {
                    return NotFound("No chats found for the user.");
                }

                var chatsDTO = Mapper.Map<List<ChatDTO>>(chats);

                return Ok(chatsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving chats: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpGet("[action]/{senderID}+{receiverID}")]
        [ProducesResponseType(200, Type = typeof(ChatDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetChatByRSID(int senderID, int receiverID)
        {
            try
            {
                var isExist = await RepositoryManager.ChatRepository.FindBool(c => (c.SenderId == senderID && c.ReceiverId == receiverID) || (c.SenderId == receiverID && c.ReceiverId == senderID));
                if (!(bool)isExist)
                {
                    return NotFound();
                }
                var chat = await RepositoryManager.ChatRepository.Find(c => (c.SenderId == senderID && c.ReceiverId == receiverID) || (c.SenderId == receiverID && c.ReceiverId == senderID));

                var chatDTO = Mapper.Map<ChatDTO>(chat);
                return Ok(chatDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Chat: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddChat([FromBody] ChatDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<Chat>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.ChatRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.ChatRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding chat. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new chat: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{chatID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditChat(int chatID, [FromBody] ChatDTO objDTO)
        {
            try
            {
                if (chatID != objDTO.ChatId)
                {
                    return BadRequest("Invalid Chat ID.");
                }

                var existingObj = await RepositoryManager.ChatRepository.GetById(chatID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<Chat>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid Chat data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.ChatRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.ChatRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing Chat. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing Chat: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{chatID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteChat(int chatID)
        {
            try
            {
                var existingObj = await RepositoryManager.ChatRepository.GetById(chatID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.ChatRepository.Remove(existingObj);
                var res = await RepositoryManager.ChatRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting Chat. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting Chat: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }





    }
}
