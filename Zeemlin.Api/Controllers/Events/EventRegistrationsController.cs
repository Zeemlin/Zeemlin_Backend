using Microsoft.AspNetCore.Mvc;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Events.EventRegistrations;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Events.EventsRegistrations;
using Zeemlin.Service.Services.Events;

namespace Zeemlin.Api.Controllers.Events;

public class EventRegistrationsController : BaseController
{
    private readonly IEventRegistrationService _eventRegistration;

    public EventRegistrationsController(IEventRegistrationService eventRegistration)
    {
        _eventRegistration = eventRegistration;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] EventRegistrationCreationDto dto)
      => Ok(await _eventRegistration.CreateAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(PaginationParams @params)
      => Ok(await _eventRegistration.GetAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
      => Ok(await _eventRegistration.GetByIdAsync(id));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
      => Ok(await _eventRegistration.DeleteAsync(id));
    [HttpGet("{EventId}-participants")]
    public async Task<IActionResult> GetByEventIdAsync(long eventId, PaginationParams @params)
    {
        try
        {
            var registrations = await _eventRegistration.GetByEventIdAsync(eventId, @params);
            return Ok(registrations);
        }
        catch (ZeemlinException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
