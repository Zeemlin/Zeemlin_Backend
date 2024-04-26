using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Data.IRepositries.Assets;
using Zeemlin.Domain.Entities.Assets;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Commons.Helpers;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Assets.VideoLessonAssets;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Assets;

namespace Zeemlin.Service.Services.Assets;

public class VideoLessonAssetService : IVideoLessonAssetService
{
    private readonly IMapper _mapper;
    private readonly IVideoLessonAssetRepository _repository;
    private readonly ILessonRepository _lessonRepository;
    private readonly string[] _allowedExtensions = { ".mp4", ".mkv", ".mov", ".webm", ".avi", ".ogv" }; // Video formats
    private readonly long _maxSizeInBytes;

    public VideoLessonAssetService(
        IMapper mapper,
        IVideoLessonAssetRepository repository,
        ILessonRepository lessonRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _maxSizeInBytes = 100 * 1024 * 1024; // Video size 100MB
        _lessonRepository = lessonRepository;
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
            throw new ZeemlinException(400, "Invalid image format. Only jpg, jpeg, and png are allowed.");
        }

    }
    public async Task<bool> DeletePictureAsync(long VideoLessonId)
    {
        var videoLesson = await _repository.SelectAll()
            .Where(vl => vl.Id == VideoLessonId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if ( videoLesson == null)
            throw new ZeemlinException(404, "Video Lesson not found");

        await _repository.DeleteAsync(VideoLessonId);
        return true;
    }

    public async Task<IEnumerable<VideoLessonAssetForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var AllVideoLessons = await _repository.SelectAll().AsNoTracking().ToPagedList(@params).ToListAsync();

        return _mapper.Map<IEnumerable<VideoLessonAssetForResultDto>>(AllVideoLessons);
    }

    public async Task<VideoLessonAssetForResultDto> RetrieveByIdAsync(long id)
    {
        var videoLesson = await _repository.SelectAll()
            .Where(vl => vl.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (videoLesson == null)
            throw new ZeemlinException(404, "Video Lesson not found");

        return _mapper.Map<VideoLessonAssetForResultDto>(videoLesson);
    }

    public async Task<VideoLessonAssetForResultDto> ModifyAsync(long id,VideoLessonAssetForUpdateDto dto)
    {
        var videoLesson = await _repository.SelectAll()
            .Where(vl => vl.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (videoLesson == null)
            throw new ZeemlinException(404, "Video Lesson not found");

        var mapped = _mapper.Map(dto, videoLesson);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(mapped);

        return _mapper.Map<VideoLessonAssetForResultDto>(mapped);
    }

    public async Task<VideoLessonAssetForResultDto> UploadAsync(VideoLessonAssetForCreationDto dto)
    {
        var IsLessonId = await _lessonRepository.SelectAll()
            .Where(l => l.Id == dto.LessonId) .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsLessonId is null)
            throw new ZeemlinException(404, "Lesson not found");

        await ValidateImageAsync(dto.Path);
        var WwwRootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "VideoLessonAssets");
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Path.FileName);
        var fullPath = Path.Combine(WwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await dto.Path.CopyToAsync(stream);
        }

        string resultImage = Path.Combine("VideoLessonAssets", fileName);

        var mapped = _mapper.Map<VideoLessonAsset>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.UploadedDate = DateTime.UtcNow;
        mapped.Path = resultImage;
        mapped.Size = dto.Path.Length;
        mapped.ContentType = dto.Path.ContentType;
        await _repository.InsertAsync(mapped);

        return _mapper.Map<VideoLessonAssetForResultDto>(mapped);
    }

    public async Task<IEnumerable<VideoLessonAssetForResultDto>> GetVideoLessonsByLessonIdAsync(long lessonId, PaginationParams @params)
    {
        var videoLessons = await _lessonRepository.SelectAll()
            .Where(vl => vl.Id == lessonId)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        if (videoLessons is null)
            throw new ZeemlinException(404, "Lesson not found");

        return _mapper.Map<IEnumerable<VideoLessonAssetForResultDto>>(videoLessons);
    }
}
