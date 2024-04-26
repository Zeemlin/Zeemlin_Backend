using System.ComponentModel.DataAnnotations;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Commons.Attributes;

namespace Zeemlin.Service.DTOs.Users.Parents;

public class ParentForCreationDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
    public GenderType Gender { get; set; }

    // Contact information:
    [Required]
    [PhoneNumber]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Authentication:
    [Required]
    [StrongPassword]
    public string Password { get; set; }

    // Address 
    [Required]
    [MaxLength(50)]
    public string DistrictName { get; set; }
    [Required]
    [MaxLength(50)]
    public string GeneralAddressMFY { get; set; }
    [Required]
    [MaxLength(50)]
    public string StreetName { get; set; }
    [Required]
    public short HouseNumber { get; set; }
}
