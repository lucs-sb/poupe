using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Domain.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task DeleteByUserId(Guid userId);
}
