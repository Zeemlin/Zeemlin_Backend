using System.ComponentModel.DataAnnotations;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Commons.Attributes;

namespace Zeemlin.Service.DTOs.Users.SuperAdmins;

public class SuperAdminForCreationDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string FirstName { get; set; } 

    [Required]
    public string LastName { get; set; } 

    [Required]
    public string Email { get; set; } 

    [Required]
    [StrongPassword]
    public string Password { get; set; } 
    public GenderType Gender { get; set; }
    [Required]
    [UzbekistanPassport]
    public string PassportSeria { get; set; }
}
