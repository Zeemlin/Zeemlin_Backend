namespace Zeemlin.Service.DTOs.Group;

public class SearchGroupResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long CourseId { get; set; }
    public string CourseName { get; set; }
    public string TeacherFirstName { get; set; }
    public string TeacherLastName { get; set; }
    public int TotalTeacherCount { get; set; }
    public int StudentCount { get; set; }

    public ICollection<GroupDataResultDto> GroupData { get; set; }
}
