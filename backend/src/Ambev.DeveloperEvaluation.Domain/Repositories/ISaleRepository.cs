using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task<Sale?> GetByIdAsync(Guid id);
        Task<List<Sale>> GetAllAsync();
        Task SaveChangesAsync();
        Task DeleteAsync(Sale sale);
        IQueryable<Sale> Query();
    }
}
