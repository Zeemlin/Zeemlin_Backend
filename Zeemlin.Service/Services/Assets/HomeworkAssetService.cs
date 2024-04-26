using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Data.IRepositries.Assets;
using Zeemlin.Domain.Entities.Assets;
using Zeemlin.Service.Commons.Helpers;
using Zeemlin.Service.DTOs.Assets.HomeworkAssets;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Assets;

namespace Zeemlin.Service.Services.Assets;

public class HomeworkAssetService : IHomeworkAssetService
{
    private readonly IMapper _mapper;
    private readonly long _maxSizeInBytes; // Faylning maximal hajmi
    private readonly IHomeworkAssetRepository _repository;
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", // Photo formatlar
        ".pdf", ".csv", // Document formatlar
        ".docx", ".txt", ".pptx", ".ppt", ".xlsx", // Windows ilovalarining fayl formatlari
        ".mp3", ".wav", // Audio formatlar
        ".mp4", ".mkv" }; // Video formatlar
    public HomeworkAssetService(IMapper mapper,
        IHomeworkAssetRepository repository,
        IHomeworkRepository homeworkRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _homeworkRepository = homeworkRepository;
        _maxSizeInBytes = 100 * 1024 * 1024; // Fayl maximal hajmi 100MB
    }

    private async Task ValidateImageAsync(IFormFile file)
    {
        if (file.Length > _maxSizeInBytes)
        {
            throw new ZeemlinException(400, "The file size exceeds the maximum allowed size.");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!_allowedExtensions.Contains(extension))
        {
            throw new ZeemlinException(400, "Invalid format. Only jpg, jpeg, png, pdf, csv, docx, txt, pptx, ppt, xlsx, mp3, wav, mp4, mkv are allowed.");
        }

    }

    public async Task<HomeworkAssetForResultDto> UploadAsync(HomeworkAssetForCreationDto dto)
    {

        var IsValidHomeworkId = await _homeworkRepository.SelectAll()
            .Where(h => h.Id == dto.HomeworkId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsValidHomeworkId is null)
            throw new ZeemlinException(404, "Homework not found");

        // 1ta uyga vazifa uchun 10 ta fayl yuklanganini tekshirish
        var existingAssetCount = await _repository.CountAsync(a => a.HomeworkId == dto.HomeworkId);
        if (existingAssetCount >= 10)
        {
            throw new ZeemlinException(400, $"You can only upload up to 10 assets per homework.");
        }

        await ValidateImageAsync(dto.Path);
        var WwwRootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "HomeworkAssets");
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Path.FileName);
        var fullPath = Path.Combine(WwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await dto.Path.CopyToAsync(stream);
        }

        string resultImage = Path.Combine("HomeworkAssets", fileName);

        var mappedHomeworkAsset = _mapper.Map<HomeworkAsset>(dto);
        mappedHomeworkAsset.CreatedAt = DateTime.UtcNow;
        mappedHomeworkAsset.UploadedDate = DateTime.UtcNow;
        mappedHomeworkAsset.Path = resultImage;
        await _repository.InsertAsync(mappedHomeworkAsset);

        return _mapper.Map<HomeworkAssetForResultDto>(mappedHomeworkAsset);
    }


    public async Task<bool> RemoveAsync(long id)
    {
        var update = await _repository.SelectAll()
           .AsNoTracking()
           .Where(u => u.Id == id)
           .FirstOrDefaultAsync();
        if (update is null)
            throw new ZeemlinException(404, "Homework not found");

        await _repository.DeleteAsync(id);
        return true; 
    }

    public async Task<IEnumerable<HomeworkAssetForResultDto>> RetrieveAllAsync()
    {
        var assets = await _repository.SelectAll().AsNoTracking().ToListAsync();

        return _mapper.Map<IEnumerable<HomeworkAssetForResultDto>>(assets);
    }

    public async Task<HomeworkAssetForResultDto> RetrieveByIdAsync(long id)
    {
        var update = await _repository.SelectAll()
           .AsNoTracking()
           .Where(u => u.Id == id)
           .FirstOrDefaultAsync();
        if (update is null)
            throw new ZeemlinException(404, "Homework not found");

        return _mapper.Map<HomeworkAssetForResultDto>(update);
    }
}
