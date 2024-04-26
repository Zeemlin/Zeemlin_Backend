using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.ParentStudents;

namespace Zeemlin.Service.Interfaces;

public interface IParentStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<ParentStudentForResultDto> RetrieveByIdAsync(long id);
    Task<ParentStudentForResultDto> AddAsync(ParentStudentForCreationDto dto);
    Task<ParentStudentForResultDto> ModifyAsync(long id, ParentStudentForUpdateDto dto);
    Task<IEnumerable<ParentStudentForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
