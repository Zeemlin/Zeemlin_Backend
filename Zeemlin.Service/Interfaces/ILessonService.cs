using Zeemlin.Service.DTOs.Lesson;

namespace Zeemlin.Service.Interfaces;

public interface ILessonService
{
    Task<bool> RemoveAsync(long id);
    Task<LessonForResultDto> RetrieveIdAsync(long id);
    Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync();
    Task<LessonForResultDto> CreateAsync(LessonForCreationDto dto);
    Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto);
    //Task<IEnumerable<RecentHomeworkSummaryDto>> GetRecentHomeworkSummariesAsync(long lessonId);
}
