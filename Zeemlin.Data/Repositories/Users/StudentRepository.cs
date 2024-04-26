using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.DbContexts;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities.Users;

namespace Zeemlin.Data.Repositories.Users;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly DbSet<Student> _students; // Add this line

    public StudentRepository(AppDbContext dbContext) : base(dbContext)
    {
        _students = dbContext.Students; // Initialize _students here
    }

    public async Task<bool> ExistsAsync(string studentUniqueId)
    {
        return await _students.AnyAsync(s => s.StudentUniqueId == studentUniqueId);
    }
    public async Task<string> GetNameByStudentIdAsync(long studentId)
    {
        var studentDetails = await _students
            .Where(s => s.Id == studentId)
            .Select(s => s.FirstName)
            .FirstOrDefaultAsync();

        return studentDetails;
    }

    public async Task<string> GetEmailByStudentIdAsync(long studentId)
    {
        var student = await _students
            .Where(s => s.Id == studentId)
            .Select(s => s.Email) // Project to only Email property
            .FirstOrDefaultAsync();

        return student.ToString(); // Return email address (null if not found)
    }

}

