using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zeemlin.Data.DbContexts;
using Zeemlin.Data.IRepositries.Assets;
using Zeemlin.Domain.Entities.Assets;

namespace Zeemlin.Data.Repositories.Assets;

public class HomeworkAssetRepository : Repository<HomeworkAsset>, IHomeworkAssetRepository
{
    public HomeworkAssetRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> CountAsync(Expression<Func<HomeworkAsset, bool>> predicate)
    {
        return await _dbContext.HomeworkAssets.CountAsync(predicate);
    }
}
