using BankAccountAPI.Entities;
using BankAccountAPI.Models;

namespace BankAccountAPI.Services.Interface
{
    /// <summary>
    /// TransactionsService interface
    /// </summary>
    public interface ITransactionsService
    {
        /// <summary>
        /// Create New Transation
        /// </summary>
        /// <param name="transations"></param>
        /// <returns></returns>
        Task<Response> CreateNewTransation(TransactionDTO transations);
        /// <summary>
        /// Debit
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<Response> MakeDebit(string accountNumber, decimal amount);
        /// <summary>
        /// Credit
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<Response> MakeCredit(string accountNumber, decimal amount);
        /// <summary>
        /// Stan konta
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        Task<Response> GetAccountBalance(string accountNumber);
    }
}
