using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Zeemlin.Domain.Commons;
using Zeemlin.Domain.Enums;
using Zeemlin.Domain.Entities.Events;

namespace Zeemlin.Domain.Entities.Users;

public class Parent : Auditable
{
    // Basic personal information:
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
    public GenderType Gender { get; set; }

    // Contact information:
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Authentication:
    [PasswordPropertyText]
    [Required]
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


    public ICollection<ParentStudent> ParentStudents { get; set; }
    public ICollection<School> Schools { get; set;}
    public ICollection<Event> Events { get; set; }
}
