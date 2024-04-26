using Zeemlin.Service.DTOs.Group;
using Zeemlin.Service.DTOs.Users.Students;

namespace Zeemlin.Service.DTOs.StudentGroups;

public class StudentGroupForResultDto
{
    public long Id { get; set; }
    public StudentForResultDto StudentForResultDto { get; set; }
    public GroupForResultDto GroupForResultDto { get; set; }
}
