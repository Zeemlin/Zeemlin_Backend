namespace Zeemlin.Service.DTOs.Events.EventRegistrations;

public class EventRegistrationCreationDto
{
    public long EventId { get; set; }

    // Participant information:
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
