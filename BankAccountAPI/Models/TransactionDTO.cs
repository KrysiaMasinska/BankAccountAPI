using BankAccountAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models
{
    public class TransactionDTO
    {
        [Required]
        public TypeOfTransaction TransactionType { get; set; }
        [Required]
        public decimal TransactionAmount { get; set; }
        [Required]
        public string AccounNumber { get; set; }

        public Transaction ToDTO()
        {
            return new Transaction
            {
                AccounNumber = AccounNumber,
                TransactionAmount = TransactionAmount,
                TransactionType = TransactionType
            };
        }
    }
}
