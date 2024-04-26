namespace Zeemlin.Service.DTOs.Homework;

public class HomeworkForUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public long LessonId { get; set; }
}
