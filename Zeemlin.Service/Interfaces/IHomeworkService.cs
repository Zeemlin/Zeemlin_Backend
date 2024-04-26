using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Homework;

namespace Zeemlin.Service.Interfaces;

public interface IHomeworkService
{
    Task<bool> RemoveAsync(long id);
    Task<HomeworkForResultDto> RetrieveIdAsync(long id);
    Task<HomeworkForResultDto> CreateAsync(HomeworkForCreationDto dto);
    Task<HomeworkForResultDto> ModifyAsync(long id, HomeworkForUpdateDto dto);
    Task<IEnumerable<HomeworkForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<HomeworkForResultDto>> RetrieveByLessonIdAsync(long lessonId, PaginationParams @params);
}
