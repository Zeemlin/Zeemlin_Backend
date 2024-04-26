using Zeemlin.Data.DbContexts;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;

namespace Zeemlin.Data.Repositories;

public class ParentStudentRepository : Repository<ParentStudent>, IParentStudentRepository
{
    public ParentStudentRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
