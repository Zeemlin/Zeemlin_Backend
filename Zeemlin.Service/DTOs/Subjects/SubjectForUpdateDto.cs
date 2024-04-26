using System.ComponentModel.DataAnnotations;

namespace Zeemlin.Service.DTOs.Subjects;

public class SubjectForUpdateDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public long LessonId { get; set; }
}
