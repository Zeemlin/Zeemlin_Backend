using Zeemlin.Domain.Enums;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Users.Teachers;

namespace Zeemlin.Service.Interfaces.Users;

public interface ITeacherService
{
    Task<bool> RemoveAsync(long id);
    Task<TeacherForResultDto> RetrieveByIdAsync(long id);
    Task<TeacherForResultDto> CreateAsync(TeacherForCreationDto dto);
    Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto);
    Task<IEnumerable<TeacherForResultDto>> GetTeachersBySchoolAsync(long schoolId);
    Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<TeacherSearchResultDto>> SearchAsync(string searchTerm, long currentSchoolId);
    Task<IEnumerable<TeacherSearchForSuperAdmin>> SearchByPhoneOrEmailForSuperAdminsAsync(string searchTerm);
    Task<IEnumerable<FilteredTeacherDTO>> GetFilteredTeachers(ScienceType? scienceType = null, long schoolId = 0);
}
