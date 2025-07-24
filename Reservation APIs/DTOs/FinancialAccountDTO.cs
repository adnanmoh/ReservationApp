using System.ComponentModel.DataAnnotations;

namespace Reservation_APIs.DTOs
{
    public class FinancialAccountDTO
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
    }
}
