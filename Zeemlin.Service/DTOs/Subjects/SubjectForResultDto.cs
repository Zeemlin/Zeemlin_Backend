using System.Text.Json.Serialization;
using Zeemlin.Service.DTOs.Lesson;

namespace Zeemlin.Service.DTOs.Subjects;

public class SubjectForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long LessonId { get; set; }
    public LessonForResultDto Lesson { get; set; }
}
