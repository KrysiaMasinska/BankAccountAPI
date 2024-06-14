using BankAccountAPI.Entities;

namespace BankAccountAPI.Services.Interface
{
    public interface ITransactionsService
    {
        Task<Response> CreateNewTransation(Transaction transations);
        Task<Response> MakeDebit(string accountNumber, decimal amount);//obciazenia
        Task<Response> MakeCredit(string accountNumber, decimal amount);//uznanie
        Task<Response> GetAccountBalance(string accountNumber);//stan konta
    }
}
