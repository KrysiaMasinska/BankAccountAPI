namespace BankAccountAPI.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public TypeOfTransaction TransactionType { get; set; }
        public decimal TransactionAmount {  get; set; }
        public StatusOfTransaction TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; }
        public string AccounNumber { get; set; }
    }
}
