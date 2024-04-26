using System.Linq.Expressions;
using Zeemlin.Domain.Entities.Assets;

namespace Zeemlin.Data.IRepositries.Assets;

public interface IHomeworkAssetRepository : IRepository<HomeworkAsset>
{
    Task<int> CountAsync(Expression<Func<HomeworkAsset, bool>> predicate);
}
