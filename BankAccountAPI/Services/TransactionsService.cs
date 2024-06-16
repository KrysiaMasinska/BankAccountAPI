using BankAccountAPI.Entities;
using BankAccountAPI.Models;
using BankAccountAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Services
{
    public class TransactionsService : ITransactionsService
    {
        private BankAccountDbContext _dbContext;
        public TransactionsService(BankAccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>>
        public async Task<Response> CreateNewTransation(TransactionDTO transactionDTO)
        {
            using var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                Transaction transaction = transactionDTO.ToDTO();
                await _dbContext.Transactions.AddAsync(transaction);
                await _dbContext.SaveChangesAsync();
                await trans.CommitAsync();
                return ReturnResponse("Transaction was successful", transaction.TransactionAmount.ToString(), StatusOfTransaction.Success.ToString());
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>>
        public async Task<Response> MakeCredit(string accountNumber, decimal amount)
        {
            using var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                Transaction transactions = _dbContext.Transactions.FirstOrDefault(r => r.AccounNumber == accountNumber);
                if (transactions == null)
                    return null;

                transactions.TransactionAmount += amount;
                transactions.TransactionType = TypeOfTransaction.Credit;
                transactions.TransactionStatus = StatusOfTransaction.Success;
                await _dbContext.SaveChangesAsync();
                await trans.CommitAsync();
                return ReturnResponse("Amount was successfully credited", transactions.TransactionAmount.ToString(), StatusOfTransaction.Success.ToString());
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>>
        public async Task<Response> MakeDebit(string accountNumber, decimal amount)
        {
            if (amount < 0)
                return ReturnResponse("Debit amount cannot be negative.", amount.ToString(), StatusOfTransaction.Error.ToString());

            using var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                Transaction transactions = await _dbContext.Transactions.FirstOrDefaultAsync(r => r.AccounNumber == accountNumber);
                if (transactions == null)
                    return null;

                if (transactions.TransactionAmount - amount < 0)
                    return ReturnResponse("Insufficient funds.", transactions.TransactionAmount.ToString(), StatusOfTransaction.Error.ToString());

                transactions.TransactionAmount -= amount;
                transactions.TransactionType = TypeOfTransaction.Debit;
                transactions.TransactionStatus = StatusOfTransaction.Success;
                await _dbContext.SaveChangesAsync();
                await trans.CommitAsync();
                return ReturnResponse("Amount was successfully debited", transactions.TransactionAmount.ToString(), StatusOfTransaction.Success.ToString());
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc/>>
        public async Task<Response> GetAccountBalance(string accountNumber)
        {
            Transaction transactions = await _dbContext.Transactions.FirstOrDefaultAsync(r => r.AccounNumber == accountNumber);
            if (transactions is null)
                return null;

            return ReturnResponse($"Account balance for {accountNumber} is {transactions.TransactionAmount}", transactions.TransactionAmount.ToString(), StatusOfTransaction.Success.ToString());
        }

        private Response ReturnResponse(string message, string data, string code)
        {
            return new Response()
            {
                Message = message,
                Data = data,
                Code = code
            };
        }
    }
}
