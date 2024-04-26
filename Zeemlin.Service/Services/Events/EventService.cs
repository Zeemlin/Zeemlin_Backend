using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries.Events;
using Zeemlin.Data.IRepositries.Users;
using Zeemlin.Domain.Entities;
using Zeemlin.Domain.Entities.Events;
using Zeemlin.Domain.Enums.Events;
using Zeemlin.Service.DTOs.Assets.EventAssets;
using Zeemlin.Service.DTOs.Events;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;
using Zeemlin.Service.Interfaces.Events;

namespace Zeemlin.Service.Services.Events;

public class EventService : IEventService
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly ISuperAdminRepository _superAdminRepository;
    private readonly IEmailService _emailService;

    public EventService(
        IMapper mapper,
        IEventRepository eventRepository,
        IAdminRepository adminRepository,
        ISuperAdminRepository superAdminRepository,
        IEmailService emailService)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
        _adminRepository = adminRepository;
        _superAdminRepository = superAdminRepository;
        _emailService = emailService;
    }

    public async Task<EventForResultDto> CreateEpicEventAsync(EventForCreationDto createDto)
    {
        // Validate all data upfront
        await ValidateEventDates(createDto); // Ensure asynchronous validation completes



        // Proceed with event creation if all validation passes
        var mappedEvent = _mapper.Map<Event>(createDto);
        mappedEvent.CreatedAt = DateTime.UtcNow; // Use DateTime.Now for user's local time zone (optional)
        mappedEvent.Status = EventStatus.InProcess;

        await _eventRepository.InsertAsync(mappedEvent);
        return _mapper.Map<EventForResultDto>(mappedEvent);
    }

    private async Task ValidateEventDates(EventForCreationDto createDto)
    {
        if (createDto.StartedAt < DateTime.Now)
        {
            throw new ZeemlinException(400, "Whoa! Start date can't be in the past. Let's schedule for the future.");
        }

        if (createDto.EndDate <= createDto.StartedAt)
        {
            throw new ZeemlinException(400, "The event needs to end sometime after it starts. Just sayin'.");
        }

        if (createDto.IsPaid && (!createDto.Price.HasValue || createDto.Price.Value < 1000))
        {
            throw new ZeemlinException(400, "Hold on there! For paid events, price can't be empty or less than 1000.");
        }

        // Assuming a username property exists in the DTO
        if (string.IsNullOrEmpty(createDto.CreatedByUsername))
        {
            throw new ZeemlinException(400, "Username cannot be empty.");
        }

        var superAdminExists = await _superAdminRepository.ExistsByUsernameAsync(createDto.CreatedByUsername);
        var adminExists = await _adminRepository.ExistsByUsernameAsync(createDto.CreatedByUsername);

        if (!adminExists && !superAdminExists)
        {
            throw new ZeemlinException(400, "Invalid username. No admin or super admin found.");
        }

    }


    public async Task<bool> RemoveEventAsync(long eventId)
    {
        var IsValidId = await _eventRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == eventId)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");

        await _eventRepository.DeleteAsync(eventId);
        return true;
    }

    public async Task<IEnumerable<EventForPublicDto>> GetApprovedEventsForPublicAsync()
    {
        // Use Include to eagerly load the EventAsset
        var events = await _eventRepository
            .SelectAll()
            .Include(e => e.EventAsset)
            .AsNoTracking()
            .Where(e => e.Status == EventStatus.Approved && 
            e.EndDate > DateTime.UtcNow)
            .ToListAsync();

        // Map the retrieved events to the DTO list and include nested DTOs
        var result = events.Select(e =>
        {
            var eventDto = _mapper.Map<EventForPublicDto>(e); // Map individual entity
            eventDto.EventAssetForResultDto = e.EventAsset != null ? _mapper.Map<EventAssetForResultDto>(e.EventAsset) : null;
            return eventDto; // Return the mapped DTO
        });

        return result;
    }





    public async Task<EventForResultDto> GetEventByIdAsync(long eventId)
    {
        // Use Include to eagerly load the EventAsset
        var eventData = await _eventRepository.SelectAll()
            .Include(e => e.EventAsset)
            .AsNoTracking()
            .Where(u => u.Id == eventId)
            .FirstOrDefaultAsync();

        if (eventData is null)
        {
            throw new ZeemlinException(404, "Not Found");
        }

        // Map the Event entity to the DTO
        var eventDto = _mapper.Map<EventForResultDto>(eventData);

        // If an EventAsset is associated, map it to the nested DTO
        if (eventData.EventAsset != null)
        {
            eventDto.EventAssetForResultDto = _mapper.Map<EventAssetForResultDto>(eventData.EventAsset);
        }

        return eventDto;
    }


    public async Task<IEnumerable<EventForResultDto>> RetrieveAllInProccesAsync()
    {
        // Use Include to eagerly load the EventAsset
        var events = await _eventRepository
            .SelectAll()
            .Include(e => e.EventAsset)
            .AsNoTracking()
            .Where(e => e.Status == EventStatus.InProcess)
            .ToListAsync();

        // Map the retrieved events to the DTO list and include nested DTOs
        var result = events.Select(e =>
        {
            var eventDto = _mapper.Map<EventForResultDto>(e); // Map individual entity
            eventDto.EventAssetForResultDto = e.EventAsset != null ? _mapper.Map<EventAssetForResultDto>(e.EventAsset) : null;
            return eventDto; // Return the mapped DTO
        });

        return result;
    }




    public async Task<EventForResultDto> UpdateEventAsync(long eventId, EventForUpdateDto updateDto)
    {
        var IsValidId = await _eventRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == eventId)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");


        var mapped = _mapper.Map(updateDto, IsValidId);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _eventRepository.UpdateAsync(mapped);

        return _mapper.Map<EventForResultDto>(mapped);
    }

    public async Task<EventForResultDto> UpdateEventStatusAsync(long eventId, EventStatusUpdateDto statusDto)
    {
        var IsValidId = await _eventRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == eventId)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");

        var IsSuperAdminId = await _superAdminRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == statusDto.UpdaterId)
            .FirstOrDefaultAsync();

        if (IsSuperAdminId is null)
            throw new ZeemlinException(404, "Super Admin not Found");

        IsValidId.Status = statusDto.NewStatus;
        var originalIsPaid = IsValidId.IsPaid;

        var mapped = _mapper.Map(statusDto, IsValidId);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.IsPaid = originalIsPaid;
        await _eventRepository.UpdateAsync(mapped);

        var creatorUsername = mapped.CreatedByUsername;
        var createrEmail = await GetCreatorEmailAsync(creatorUsername);


        // Send email notification
        await SendEmailNotification(createrEmail, creatorUsername, statusDto.NewStatus, mapped);

        return _mapper.Map<EventForResultDto>(mapped);
    }

    public async Task<IEnumerable<RejectedEventForSuperAdminDto>> RetrieveAllRejectedAsync()
    {
        // Fetch rejected events with non-null UpdaterIds
        var rejectedEvents = await _eventRepository.SelectAll()
          .Where(e => e.Status == EventStatus.Rejected && e.UpdaterId.HasValue)  // Filter by non-null UpdaterId
          .AsNoTracking()
          .ToListAsync();

        // Separate query to retrieve usernames for UpdaterIds
        var superAdminUsernames = await _superAdminRepository.GetAllUsernamesByIds(
          rejectedEvents.Select(e => e.UpdaterId.Value).ToList()); // Use Value property for non-null UpdaterIds

        // Map events and populate username
        var mappedEvents = rejectedEvents.Select(e =>
        {
            var username = superAdminUsernames.ContainsKey(e.UpdaterId.Value) ?
              superAdminUsernames[e.UpdaterId.Value] : null;
            var dto = _mapper.Map<RejectedEventForSuperAdminDto>(e);
            dto.UpdaterUsername = username;
            return dto;
        });


        return mappedEvents;
    }



    public async Task<IEnumerable<ApprovedEventForSuperAdminDto>> RetrieveAllApprovedAsync()
    {
        var approvedEvents = await _eventRepository.SelectAll()
            .Where(e => e.Status == EventStatus.Approved)
            .AsNoTracking()
            .ToListAsync();

        var superAdminUsernames = await _superAdminRepository.GetAllUsernamesByIds(
          approvedEvents.Select(e => e.UpdaterId.Value).ToList()); // Use Value property for non-null UpdaterIds

        // Map events and populate username
        var mappedEvents = approvedEvents.Select(e =>
        {
            var username = superAdminUsernames.ContainsKey(e.UpdaterId.Value) ?
              superAdminUsernames[e.UpdaterId.Value] : null;
            var dto = _mapper.Map<ApprovedEventForSuperAdminDto>(e);
            dto.UpdaterUsername = username;
            return dto;
        });


        return mappedEvents;

    }



    private async Task<string> GetCreatorEmailAsync(string username)
    {
        // Assuming creatorId belongs to either admin or super admin
        var superAdminEmail = await _superAdminRepository.GetEmailByUsername(username);

        if (string.IsNullOrEmpty(superAdminEmail))
        {
            var adminEmail = await _adminRepository.GetEmailByUsername(username); // Assuming username is stored as string
            return adminEmail;
        }

        // Return Super Admin email if found
        return superAdminEmail;
    }

    private async Task SendEmailNotification(string createrEmail ,string creatorUsername, EventStatus newStatus, Event eventData)
    {
        var subject = $"Your event '{eventData.Title}' has been {newStatus.ToString().ToLower()}";
        var body = $"Dear {creatorUsername},\n\n" +
                    $"\n This email is to inform you that your event '{eventData.Title}' has been {newStatus.ToString().ToLower()}.\n";

        var message = new Message
        {
            Subject = subject,
            Body = body,
            To = createrEmail
        };

        await _emailService.SendMessage(message);
    }
}
