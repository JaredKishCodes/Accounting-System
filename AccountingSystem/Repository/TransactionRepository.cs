
using AccountingSystem.Data;
using AccountingSystem.Interfaces;
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
           var result =  await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Transactions.ToListAsync();
            }

            // Corrected parentheses and ensured proper LINQ usage
            return await _context.Transactions
                .Where(t => EF.Functions.Like(t.Description, $"%{searchTerm}%"))
                .ToListAsync();
        }
    }
}
