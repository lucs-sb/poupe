using Microsoft.EntityFrameworkCore;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Repositories;
using Poupe.Infrastructure.Repositories.Base;

namespace Poupe.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext) { }

    public new async Task<List<UserResponseDTO>> GetAllAsync()
    {
        return await _dbContext.Database.SqlQueryRaw<UserResponseDTO>(@"
            SELECT
                u.id,
                u.name,
                u.age,
                COALESCE(SUM(CASE WHEN t.type = 0 THEN t.value ELSE 0 END), 0) AS incomes,
                COALESCE(SUM(CASE WHEN t.type = 1 THEN t.value ELSE 0 END), 0) AS expenses,
                COALESCE(SUM(CASE WHEN t.type = 0 THEN t.value ELSE 0 END), 0)
                    - COALESCE(SUM(CASE WHEN t.type = 1 THEN t.value ELSE 0 END), 0) AS balance
            FROM
                PoupeDB.tb_user u
                LEFT JOIN PoupeDB.tb_transaction t ON t.user_id = u.id
            GROUP BY
                u.id,
                u.name,
                u.age
        ").ToListAsync();
    }

    public new async Task<UserResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Database.SqlQueryRaw<UserResponseDTO>(@"
            SELECT
                u.id,
                u.name,
                u.age,
                COALESCE(SUM(CASE WHEN t.type = 0 THEN t.value ELSE 0 END), 0) AS incomes,
                COALESCE(SUM(CASE WHEN t.type = 1 THEN t.value ELSE 0 END), 0) AS expenses,
                COALESCE(SUM(CASE WHEN t.type = 0 THEN t.value ELSE 0 END), 0)
                    - COALESCE(SUM(CASE WHEN t.type = 1 THEN t.value ELSE 0 END), 0) AS balance
            FROM
                PoupeDB.tb_user u
                LEFT JOIN PoupeDB.tb_transaction t ON t.user_id = u.id
            WHERE
                u.id = {0}
            GROUP BY
                u.id,
                u.name,
                u.age
        ", id).SingleOrDefaultAsync();
    }
}
