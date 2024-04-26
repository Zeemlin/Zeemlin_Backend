using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Events.EventRegistrations;

namespace Zeemlin.Service.Interfaces.Events.EventsRegistrations;

public interface IEventRegistrationService
{
    Task<bool> DeleteAsync(long id);
    Task<EventRegistrationResultDto> GetByIdAsync(long id);
    Task<EventRegistrationResultDto> SearchByCodeAsync(string code);
    Task<EventRegistrationResultDto> CreateAsync(EventRegistrationCreationDto dto);
    Task<ICollection<EventRegistrationResultDto>> GetAllAsync(PaginationParams @params);
    Task<ICollection<EventRegistrationResultDto>> GetByEventIdAsync(long eventId, PaginationParams @params);
}

