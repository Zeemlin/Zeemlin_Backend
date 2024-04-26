using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Subjects;

namespace Zeemlin.Service.Interfaces;

public interface ISubjectService
{
    Task<bool> RemoveAsync(long id);
    Task<SubjectForResultDto> RetrieveByIdAsync(long id);
    Task<SubjectForResultDto> CreateAsync(SubjectForCreationDto dto);
    Task<SubjectForResultDto> ModifyAsync(long id, SubjectForUpdateDto dto);
    Task<IEnumerable<SubjectForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<SubjectForResultDto>> RetrieveSubjectsByLessonIdAsync(long lessonId, PaginationParams @params);
}
