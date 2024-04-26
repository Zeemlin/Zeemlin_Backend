using System.ComponentModel.DataAnnotations;
using Zeemlin.Domain.Commons;

namespace Zeemlin.Domain.Entities.Events;

public class EventRegistration : Auditable
{
    [Required]
    public long EventId { get; set; }
    public Event Event { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string RegistrationCode { get; set; }
}
