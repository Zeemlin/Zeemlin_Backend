using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.DbContexts;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;

namespace Zeemlin.Data.Repositories;

public class HomeworkRepository : Repository<Homework>, IHomeworkRepository
{
    public HomeworkRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Homework>> GetForLessonAsync(long lessonId)
    {
        // Assuming Homework has a foreign key relationship with Lesson
        return await _dbContext.Homework
            .Include(hw => hw.Lesson) // Eager loading for Lesson data (optional)
            .Where(hw => hw.LessonId == lessonId)
            .ToListAsync();
    }

}
