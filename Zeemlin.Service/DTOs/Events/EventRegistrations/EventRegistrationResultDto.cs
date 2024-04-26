namespace Zeemlin.Service.DTOs.Events.EventRegistrations;

public class EventRegistrationResultDto
{
    public long Id { get; set; }
    public long EventId { get; set; }

    // Participant information:
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string RegistrationDate { get; set; }
    public string RegistrationCode { get; set; }
}
