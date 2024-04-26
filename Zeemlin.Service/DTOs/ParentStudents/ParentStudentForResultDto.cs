using Zeemlin.Service.DTOs.Users.Parents;
using Zeemlin.Service.DTOs.Users.Students;

namespace Zeemlin.Service.DTOs.ParentStudents;

public class ParentStudentForResultDto
{
    public long Id { get; set; }
    public long ParentId { get; set; }
    public ParentForResultDto Parent {  get; set; }
    public long StudentId { get; set; }
    public StudentForResultDto Student { get; set; }
}
