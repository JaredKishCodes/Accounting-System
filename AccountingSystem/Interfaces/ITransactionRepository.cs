

using AccountingSystem.Models;

namespace AccountingSystem.Interfaces
{
    public interface ITransactionRepository

    {
        public Task<IEnumerable<Transaction>> GetTransactionsAsync();
        public Task<IEnumerable<Transaction>> SearchTransactionsAsync(string searchTerm);

        public Task CreateTransactionAsync(Transaction transaction);

    }
}
