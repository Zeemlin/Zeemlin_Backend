using Zeemlin.Domain.Entities.Users;

namespace Zeemlin.Data.IRepositries;

public interface IStudentRepository : IRepository<Student>
{
    Task<bool> ExistsAsync(string studentUniqueId);
    Task<string> GetNameByStudentIdAsync(long studentId);
    Task<string> GetEmailByStudentIdAsync(long studentId);
}
