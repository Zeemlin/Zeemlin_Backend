using Zeemlin.Data.DbContexts;
using Zeemlin.Data.IRepositries.Users;
using Zeemlin.Domain.Entities.Users;

namespace Zeemlin.Data.Repositories.Users;

public class ParentRepository : Repository<Parent>, IParentRepository
{
    public ParentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
