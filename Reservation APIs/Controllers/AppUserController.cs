using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;
using Reservation_APIs.Repositories;

namespace Reservation_APIs.Controllers
{
     
    public class AppUserController : BaseController
    {


        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUserDTO>))] 
        [ProducesResponseType(500)] 
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await RepositoryManager.AppUserRepository.GetAll();

                if (users == null || !users.Any())
                {
                    return NotFound();
                }

                var usersDTO = Mapper.Map<List<AppUserDTO>>(users);

                return Ok(usersDTO); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving users: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request."); 
            }
        }



        [HttpGet("[action]/{id}")]
        [ProducesResponseType(200, Type = typeof(AppUserDTO))] 
        [ProducesResponseType(404)] 
        [ProducesResponseType(500)] 
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                if (!await RepositoryManager.AppUserRepository.ObjExists(id))
                {
                    return NotFound();
                }

                var user = await RepositoryManager.AppUserRepository.GetById(id);

                var userDTO = Mapper.Map<AppUserDTO>(user);

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]/{phone}")]
        [ProducesResponseType(200, Type = typeof(AppUserDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserByPhone(String phone)
        {
            try
            {
                var user = await RepositoryManager.AppUserRepository.Find(u => u.Phone == phone);
                if (user == null)
                {
                    return NotFound();
                }


                var userDTO = Mapper.Map<AppUserDTO>(user);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("[action]/{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<string> IsUserEmailUsed(string email)
        {
            try
            {
                var user = await RepositoryManager.AppUserRepository.Find(u => u.Email == email);
                if (user == null)
                {
                    return "no";
                }




                return "yes";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return "error";
            }
        }

        [HttpGet("[action]/{phone}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public async Task<string> IsUserPhoneUsed(string phone)
        {
            try
            {
                var user = await RepositoryManager.AppUserRepository.Find(u => u.Phone == phone);

                if (user ==null)
                {
                    return "no";
                }


                

                return "yes";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return "error";
            }
        }


        [HttpGet("[action]/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> IsApproved(int id)
        {
            try
            {
                if (!await RepositoryManager.AppUserRepository.ObjExists(id))
                {
                    return NotFound();
                }

                var approvedUser = await RepositoryManager.AppUserRepository.Find(u => u.IsApproved == true && u.UserId == id);

                if (approvedUser !=null)
                {
                    return Ok("User is approved.");
                }
                else
                {
                    return BadRequest("User is not approved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet("[action]/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserType(int id)
        {
            try
            {
                if (!await RepositoryManager.AppUserRepository.ObjExists(id))
                {
                    return NotFound();
                }

                var user = await RepositoryManager.AppUserRepository.GetById(id);

                var type = await RepositoryManager.UserTypeRepository.Find(x => x.UserTypeId == user.UserTypeId);
                if (type != null)
                {
                    return Ok(type.Name);
                }
                else
                {
                    return BadRequest("User is not approved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)] // يوجد تضارب مع المستخدمين الموجودين بالفعل
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddUser([FromBody] AppUserDTO objDTO)
        {
            try
            {
                var existingEmailUser = await RepositoryManager.AppUserRepository.Find(u => u.Email == objDTO.Email);
                if (existingEmailUser !=null)
                {
                    return Conflict("Email is already in use.");
                }

                // فحص توافر رقم الهاتف
                var existingPhoneUser = await RepositoryManager.AppUserRepository.Find(u => u.Phone == objDTO.Phone);
                if (existingPhoneUser != null)
                {
                    return Conflict("Phone number is already in use."); 
                }
                objDTO.Password = BCrypt.Net.BCrypt.HashPassword(objDTO.Password);
                var obj = Mapper.Map<AppUser>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid User data."); 
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState); 
                }

                var res = await RepositoryManager.AppUserRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.AppUserRepository.SaveChangesAsync();
                    return Ok(); 
                }

                return StatusCode(500, "There was a problem adding the user. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new user: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request."); 
            }
        }




        [HttpPut("[action]/{UserID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)] // يوجد تضارب مع مستخدم موجود بالفعل
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditUser(int UserID, [FromBody] AppUserDTO objDTO)
        {
            try
            {
                if (UserID != objDTO.UserId)
                {
                    return BadRequest("Invalid User ID.");
                }

                var existingObj = await RepositoryManager.AppUserRepository.GetById(UserID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                var existingEmailUsers = await RepositoryManager.AppUserRepository.Find(u => u.Email == objDTO.Email && u.UserId != objDTO.UserId);
                if (existingEmailUsers != null)
                {
                    return Conflict("Email is already in use by another user."); 
                }

                var existingPhoneUsers = await RepositoryManager.AppUserRepository.Find(u => u.Phone == objDTO.Phone && u.UserId != UserID);
                if (existingPhoneUsers != null)
                {
                    return Conflict("Phone number is already in use by another user."); 
                }
                objDTO.Password = BCrypt.Net.BCrypt.HashPassword(objDTO.Password);
                var obj = Mapper.Map<AppUser>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid User data.");
                }
                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.AppUserRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.AppUserRepository.SaveChangesAsync();
                     return Ok();
                }
                return StatusCode(500, "There was a problem editing the user. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing AppUser: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request."); 
            }
        }


        [HttpPut("[action]/{UserID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)] // يوجد تضارب مع مستخدم موجود بالفعل
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditUserWithOutHashing(int UserID, [FromBody] AppUserDTO objDTO)
        {
            try
            {
                if (UserID != objDTO.UserId)
                {
                    return BadRequest("Invalid User ID.");
                }

                var existingObj = await RepositoryManager.AppUserRepository.GetById(UserID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                var existingEmailUsers = await RepositoryManager.AppUserRepository.Find(u => u.Email == objDTO.Email && u.UserId != objDTO.UserId);
                if (existingEmailUsers != null)
                {
                    return Conflict("Email is already in use by another user.");
                }

                var existingPhoneUsers = await RepositoryManager.AppUserRepository.Find(u => u.Phone == objDTO.Phone && u.UserId != UserID);
                if (existingPhoneUsers != null)
                {
                    return Conflict("Phone number is already in use by another user.");
                }
                var obj = Mapper.Map<AppUser>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid User data.");
                }
                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.AppUserRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.AppUserRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing the user. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing AppUser: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        [HttpDelete("[action]/{UserID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(int UserID)
        {
            try
            {
                var existingObj = await RepositoryManager.AppUserRepository.GetById(UserID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.AppUserRepository.Remove(existingObj);
                var res = await RepositoryManager.AppUserRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting the user. Please try again.");
                }

                 return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting AppUser: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpGet("[action]/{phone}/{password}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> LogIn(string phone, string password)
        {
            try
            {
                

                var user = await RepositoryManager.AppUserRepository.Find(u => u.Phone == phone);
                if(user == null)
                {
                    return Ok("phone");
                }
                if (!await RepositoryManager.AppUserRepository.ObjExists(user.UserId))
                {
                    return NotFound("Invalid user ID.");
                }

                bool status = await RepositoryManager.AppUserRepository.LogIn(user.UserId, password);
                if (status)
                {
                    var isApproved = await RepositoryManager.AppUserRepository.Find(u => u.IsApproved == true && u.UserId == user.UserId);
                    if(isApproved != null)
                    {
                        return Ok("success");
                    }
                    return Ok("notActive");
                }
                else
                {
                    return Ok("password");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while signing in: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
