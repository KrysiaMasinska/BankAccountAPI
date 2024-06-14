using BankAccountAPI.Entities;
using BankAccountAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("api/bankaccount")]
    public class BankAccountController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        public BankAccountController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [Route("create_new_transaction")]
        [HttpPost]
        public async Task<ActionResult> CreateNewTransation(Transaction transations)
        {
            Response result = await _transactionsService.CreateNewTransation(transations);
            return ReturnResult(result);
        }

        [Route("get_account_balance")]
        [HttpGet]
        public async Task<ActionResult> GetAccountBalance(string accountNumber)
        {
            Response result = await _transactionsService.GetAccountBalance(accountNumber);
            return ReturnResult(result);
        }

        [Route("make_credit")]
        [HttpPost]
        public async Task<ActionResult> MakeCredit(string accountNumber, decimal amount)
        {
            Response result = await _transactionsService.MakeCredit(accountNumber, amount);
            return ReturnResult(result);
        }

        [Route("make_debit")]
        [HttpPost]
        public async Task<ActionResult> MakeDebit(string accountNumber, decimal amount)
        {
            Response result = await _transactionsService.MakeDebit(accountNumber, amount);
            return ReturnResult(result);
        }

        private ActionResult ReturnResult(Response result)
        {
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
