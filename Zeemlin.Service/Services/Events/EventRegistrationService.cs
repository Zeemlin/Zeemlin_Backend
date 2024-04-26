using AutoMapper;
using iTextSharp.text; // Namespace for core iTextSharp functionalities
using iTextSharp.text.pdf; // Namespace for PDF generation functionalities
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries.Events;
using Zeemlin.Domain.Entities.Events;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Events.EventRegistrations;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;
using Zeemlin.Service.Interfaces.Events.EventsRegistrations;


namespace Zeemlin.Service.Services.Events;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IEventRegistrationRepository _eventRegistrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IEmailService _emailService;

    public EventRegistrationService(
        IMapper mapper,
        IEventRegistrationRepository eventRegistrationRepository,
        IEventRepository eventRepository,
        IEmailService emailService)
    {
        _mapper = mapper;
        _eventRegistrationRepository = eventRegistrationRepository;
        _eventRepository = eventRepository;
        _emailService = emailService;
    }

    private string GenerateRegistrationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 5)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<MemoryStream> GenerateRegistrationConfirmationPdf(EventRegistration registration, Event evnt)
    {
        // Create a MemoryStream object to hold the PDF data
        var memoryStream = new MemoryStream();

        // Use iTextSharp to create a PDF document
        Document document = new Document(PageSize.A4);
        var writer = PdfWriter.GetInstance(document, memoryStream);
        document.Open();

        // Add content to the PDF (e.g., user information, event details)
        var paragraph = new Paragraph($"Registration Confirmation for {evnt.Title}");
        paragraph.Font.IsStandardFont(); // Set font size using SetFontSize method
        paragraph.Alignment = Element.ALIGN_CENTER;
        document.Add(paragraph);

        document.Add(new Paragraph(" ")); // Add some spacing

        document.Add(new Phrase($"Name: {registration.FirstName} {registration.LastName}"));
        document.Add(new Phrase($"Email: {registration.Email}"));
        document.Add(new Phrase($"Registration Code: {registration.RegistrationCode}")); // Assuming there's a RegistrationCode property in EventRegistration
        document.Add(new Phrase($"Registration Date: {registration.RegistrationDate:yyyy-MM-dd HH:mm}"));

        document.Add(new Paragraph(" ")); // Add some spacing

        document.Add(new Phrase($"Event Name: {evnt.Title}"));
        document.Add(new Phrase($"Event Time: {evnt.StartedAt:yyyy-MM-dd HH:mm} - {evnt.EndDate:yyyy-MM-dd HH:mm}"));
        document.Add(new Phrase($"Event Location: {evnt.Location}"));
        document.Add(new Phrase($"Event Address: {evnt.Address}"));

        // Format price with currency symbol (assuming Price is a decimal)
        document.Add(new Phrase($"Event Price: {evnt.Price:C}")); // Uses the current culture's currency format

        document.Close();

        // Don't close the MemoryStream here (caller will manage disposal)
        // return memoryStream.ToArray();  // Removed

        return memoryStream;
    }



    public async Task<EventRegistrationResultDto> CreateAsync(EventRegistrationCreationDto dto)
    {
        // Check if event exists
        var existingEvent = await _eventRepository.SelectAll()
            .Where(e => e.Id == dto.EventId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (existingEvent is null)
        {
            throw new ZeemlinException(404, "No such event exists");
        }

        // Check for duplicate registration using email and event ID
        //var existingRegistration = await _eventRegistrationRepository.SelectAll().Where(
        //    r => r.EventId == dto.EventId && r.Email.ToLower() == dto.Email.ToLower())
        //    .AsNoTracking()
        //    .FirstOrDefaultAsync();

        //if (existingRegistration != null)
        //{
        //    throw new ZeemlinException(409, "This email has already registered for this event.");
        //}

        // Generate registration code (replace with your preferred generation logic)

        // Map DTO to entity
        var mapped = _mapper.Map<EventRegistration>(dto);
        mapped.RegistrationCode = GenerateRegistrationCode();

        // Set registration date
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.RegistrationDate = DateTime.UtcNow;

        await _eventRegistrationRepository.InsertAsync(mapped);

        // Send confirmation email in a separate task
        await SendRegistrationConfirmationEmail(mapped, existingEvent);
        // Map entity back to DTO for returning result
        return _mapper.Map<EventRegistrationResultDto>(mapped);
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var deleteParticipant = await _eventRegistrationRepository
            .SelectAll()
            .Where(e => e.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (deleteParticipant is null)
            throw new ZeemlinException(404, "Participant not found");

        await _eventRegistrationRepository .DeleteAsync(id);
        return true;
    }

    public async Task<ICollection<EventRegistrationResultDto>> GetByEventIdAsync(long eventId, PaginationParams @params)
    {
        var deleteParticipant = await _eventRepository
            .SelectAll()
            .Where(e => e.Id == eventId)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();
        if (deleteParticipant is null)
            throw new ZeemlinException(404, "Participant not found");

        return _mapper.Map<ICollection<EventRegistrationResultDto>>(deleteParticipant);
    }

    public async Task<EventRegistrationResultDto> GetByIdAsync(long id)
    {
        var Participant = await _eventRegistrationRepository
            .SelectAll()
            .Where(e => e.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (Participant is null)
            throw new ZeemlinException(404, "Participant not found");

        return _mapper.Map<EventRegistrationResultDto>(Participant);
        throw new NotImplementedException();
    }

    public async Task<EventRegistrationResultDto> SearchByCodeAsync(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            throw new ArgumentException("Registration code cannot be null or empty.");
        }

        var registration = await _eventRegistrationRepository.SelectAll()
            .Where(r => r.RegistrationCode == code)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (registration is null)
        {
            throw new ZeemlinException(404, "Such a code does not exist or has been disabled.");
        }

        return _mapper.Map<EventRegistrationResultDto>(registration);
    }

    public async Task<ICollection<EventRegistrationResultDto>> GetAllAsync(PaginationParams @params)
    {
        var Participants = await _eventRegistrationRepository.SelectAll().AsNoTracking().ToPagedList(@params).ToListAsync();

        return _mapper.Map<ICollection<EventRegistrationResultDto>>(Participants);
    }

    private async Task SendRegistrationConfirmationEmail(EventRegistration registration, Event evnt)
    {
        // Generate the PDF data
        var memoryStream = await GenerateRegistrationConfirmationPdf(registration, evnt);

        // Prepare the email content
        var recipientEmail = registration.Email;
        var subject = $"Your Registration Confirmation for {evnt.Title}";

        // Craft a personalized message body
        var messageBody = $"Dear {registration.FirstName} {registration.LastName},\n" +
                            $"Thank you for registering for the event '{evnt.Title}'.\n";

        // Convert MemoryStream to byte array
        var pdfData = memoryStream.ToArray();

        // Send the email with the PDF attachment
        await _emailService.SendEmailWithAttachment(recipientEmail, subject, messageBody, pdfData);

        // Consider disposing the MemoryStream here (optional)
        memoryStream.Dispose();
    }


}
