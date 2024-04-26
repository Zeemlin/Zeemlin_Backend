using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries.Assets;
using Zeemlin.Data.IRepositries.Events;
using Zeemlin.Domain.Entities.Assets;
using Zeemlin.Service.Commons.Helpers;
using Zeemlin.Service.DTOs.Assets.EventAssets;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Assets;

namespace Zeemlin.Service.Services.Assets;

public class EventAssetService : IEventAssetService
{
    private readonly IMapper _mapper;
    private readonly long _maxSizeInBytes;
    private readonly IEventRepository _eventRepository;
    private readonly IEventAssetRepository _eventAssetRepository;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".HEIC" }; // Support image formats

    public EventAssetService(
        IMapper mapper,
        IEventRepository eventRepository,
        IEventAssetRepository eventAssetRepository)
    {
        _mapper = mapper;
        _maxSizeInBytes = 10 * 1024 * 1024;
        _eventRepository = eventRepository;
        _eventAssetRepository = eventAssetRepository;
    }

    private async Task ValidateImageAsync(IFormFile file)
    {
        if (file.Length > _maxSizeInBytes)
        {
            throw new ZeemlinException(400, "Image size exceeds maximum allowed size.");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(extension))
        {
            throw new ZeemlinException(400, "Invalid image format. Only jpg, jpeg, png and heic are allowed.");
        }

    }

    public async Task<bool> DeletePictureAsync(long Id)
    {
        var delete = await _eventAssetRepository.SelectAll()
           .Where(u => u.Id == Id)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (delete is null)
            throw new ZeemlinException(404, "Event Asset not found");

        await _eventAssetRepository.DeleteAsync(Id);
        return true;
    }

    public async Task<IEnumerable<EventAssetForResultDto>> RetrieveAllAsync()
    {
        var assets = await _eventAssetRepository.SelectAll().AsNoTracking().ToListAsync();

        return _mapper.Map<IEnumerable<EventAssetForResultDto>>(assets);
    }

    public async Task<EventAssetForResultDto> RetrieveByIdAsync(long Id)
    {
        var delete = await _eventAssetRepository.SelectAll()
           .Where(u => u.Id == Id)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (delete is null)
            throw new ZeemlinException(404, "Event Asset not found");

        return _mapper.Map<EventAssetForResultDto>(delete);
    }

    public async Task<EventAssetForResultDto> UploadAsync(EventAssetForCreationDto dto)
    {
        var IsValidEventId = await _eventRepository.SelectAll()
            .Include(e => e.EventAsset)
            .Where(e => e.Id == dto.EventId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsValidEventId is null)
            throw new ZeemlinException(404, "Event not found");

        if (IsValidEventId.EventAsset != null)
            throw new ZeemlinException(409, "The file for the event is already ready");

        await ValidateImageAsync(dto.Path);
        var WwwRootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "EventAssets");
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Path.FileName);
        var fullPath = Path.Combine(WwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await dto.Path.CopyToAsync(stream);
        }

        string resultImage = Path.Combine("EventAssets", fileName);

        var mapped = _mapper.Map<EventAsset>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.UploadedDate = DateTime.UtcNow;
        mapped.Path = resultImage;
        await _eventAssetRepository.InsertAsync(mapped);

        return _mapper.Map<EventAssetForResultDto>(mapped);
    }
}
