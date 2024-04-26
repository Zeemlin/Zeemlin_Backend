using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Commons.Attributes;

namespace Zeemlin.Service.DTOs.Users.Directors;

public class DirectorForCreationDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAttribute]
    public string Email { get; set; }
    [PhoneAttribute]
    public string PhoneNumber { get; set; }
    [Required]
    [StrongPassword]
    public string Password { get; set; }    
    public GenderType Gender { get; set; }
    [Required]
    [UzbekistanPassport]
    public string PassportSeria { get; set; }
}
