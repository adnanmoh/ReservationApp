using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation_APIs.DTOs;
using Reservation_APIs.Models;

namespace Reservation_APIs.Controllers
{
     
    public class FinancialAccountController : BaseController
    {
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FinancialAccountDTO>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllFinancialAccount()
        {
            try
            {
                var accounts = await RepositoryManager.FinancialAccountRepository.GetAll();

                if (accounts == null || !accounts.Any())
                {
                    return NotFound("No accounts found");
                }

                var accountsDTO = Mapper.Map<List<FinancialAccountDTO>>(accounts);

                return Ok(accountsDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving accounts: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpGet("[action]/{accountID}")]
        [ProducesResponseType(200, Type = typeof(FinancialAccountDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFinancialAccountByID(int accountID)
        {
            try
            {
                var isExist = await RepositoryManager.FinancialAccountRepository.ObjExists(accountID);
                if (!(bool)isExist)
                {
                    return NotFound();
                }
                var account = await RepositoryManager.FinancialAccountRepository.GetById(accountID);

                var accountDTO = Mapper.Map<FinancialAccountDTO>(account);
                return Ok(accountDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving FinancialAccount: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddFinancialAccount([FromBody] FinancialAccountDTO objDTO)
        {
            try
            {


                var obj = Mapper.Map<FinancialAccount>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid data.");
                }

                if (!TryValidateModel(obj))
                {
                    return BadRequest(ModelState);
                }

                var res = await RepositoryManager.FinancialAccountRepository.Add(obj);
                if (res != null)
                {
                    _ = await RepositoryManager.FinancialAccountRepository.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(500, "There was a problem adding account. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding new account: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("[action]/{accountID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EditFinancialAccount(int accountID, [FromBody] FinancialAccountDTO objDTO)
        {
            try
            {
                if (accountID != objDTO.AccountId)
                {
                    return BadRequest("Invalid FinancialAccount ID.");
                }

                var existingObj = await RepositoryManager.FinancialAccountRepository.GetById(accountID);
                if (existingObj == null)
                {
                    return NotFound();
                }


                var obj = Mapper.Map<FinancialAccount>(objDTO);
                if (obj == null)
                {
                    return BadRequest("Invalid FinancialAccount data.");
                }

                Mapper.Map(objDTO, existingObj);

                if (!TryValidateModel(existingObj))
                {
                    return BadRequest(ModelState);
                }

                var res = RepositoryManager.FinancialAccountRepository.Update(existingObj);
                if (res != null)
                {
                    _ = await RepositoryManager.FinancialAccountRepository.SaveChangesAsync();
                    return Ok();
                }
                return StatusCode(500, "There was a problem editing FinancialAccount. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while editing FinancialAccount: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [HttpDelete("[action]/{accountID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteFinancialAccount(int accountID)
        {
            try
            {
                var existingObj = await RepositoryManager.FinancialAccountRepository.GetById(accountID);
                if (existingObj == null)
                {
                    return NotFound();
                }

                RepositoryManager.FinancialAccountRepository.Remove(existingObj);
                var res = await RepositoryManager.FinancialAccountRepository.SaveChangesAsync();
                if (res <= 0)
                {
                    return StatusCode(500, "There was a problem deleting FinancialAccount. Please try again.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting FinancialAccount: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
