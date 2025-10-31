

using AccountingSystem.Models.Enum;

namespace AccountingSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public DebitAccount DebitAccount { get; set; }
        public CreditAccount CreditAccount { get; set; }
        public decimal Ammount { get; set; }
    }
}
