using System.ComponentModel.DataAnnotations;

namespace Zeemlin.Service.DTOs.ParentStudents;

public class ParentStudentForUpdateDto
{
    [Required]
    public long ParentId { get; set; }
    [Required]
    public long StudentId { get; set; }
}
