﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Data.IRepositries.Assets;
using Zeemlin.Domain.Entities.Assets;
using Zeemlin.Service.Commons.Helpers;
using Zeemlin.Service.DTOs.Assets.SchoolLogoAssets;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Assets;

namespace Zeemlin.Service.Services.Assets;

public class SchoolLogoAssetService : ISchoolLogoAssetService
{
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".HEIC" };
    private readonly ISchoolLogoAssetRepository _schoolLogoAssetRepository;
    private readonly ISchoolRepository _schoolRepository;
    private readonly long _maxSizeInBytes;
    private readonly IMapper _mapper;

    public SchoolLogoAssetService(
        ISchoolLogoAssetRepository schoolLogoAssetRepository,
        ISchoolRepository schoolRepository,
        IMapper mapper)
    {
        _schoolLogoAssetRepository = schoolLogoAssetRepository;
        _schoolRepository = schoolRepository;
        _maxSizeInBytes = 5 * 1024 * 1024;
        _mapper = mapper;
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

    public async Task<bool> DeletePictureAsync(long Id)
    {
        var delete = await _schoolLogoAssetRepository.SelectAll()
           .AsNoTracking()
           .Where(u => u.Id == Id)
           .FirstOrDefaultAsync();
        if (delete is null)
            throw new ZeemlinException(404, "School Logo not found");

        await _schoolLogoAssetRepository.DeleteAsync(Id);

        return true;
    }

    public async Task<IEnumerable<SchoolLogoAssetForResultDto>> RetrieveAllAsync()
    {
        var assets = await _schoolLogoAssetRepository.SelectAll().AsNoTracking().ToListAsync();

        return _mapper.Map<IEnumerable<SchoolLogoAssetForResultDto>>(assets);
    }

    public async Task<SchoolLogoAssetForResultDto> RetrieveByIdAsync(long Id)
    {
        var getById = await _schoolLogoAssetRepository.SelectAll()
           .AsNoTracking()
           .Where(u => u.Id == Id)
           .FirstOrDefaultAsync();
        if (getById is null)
            throw new ZeemlinException(404, "School Asset not found");

        return _mapper.Map<SchoolLogoAssetForResultDto>(getById);
    }

    public async Task<SchoolLogoAssetForResultDto> UploadAsync(SchoolLogoAssetForCreationDto dto)
    {
        var school = await _schoolRepository.SelectAll()
          .Include(s => s.SchoolLogoAsset) // Include SchoolLogoAsset in the query
          .Where(s => s.Id == dto.SchoolId)
          .AsNoTracking()
          .FirstOrDefaultAsync();

        if (school is null)
        {
            throw new ZeemlinException(404, "School not found");
        }

        // Check if a logo already exists for the school
        if (school.SchoolLogoAsset != null)
        {
            // Handle the conflict (e.g., throw an exception or update existing logo)
            throw new ZeemlinException(409, "School already has a logo. Update existing logo or choose a different school.");
        }


        await ValidateImageAsync(dto.Path);
        var WwwRootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "SchoolLogoAssets");
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(dto.Path.FileName);
        var fullPath = Path.Combine(WwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await dto.Path.CopyToAsync(stream);
        }

        string resultImage = Path.Combine("SchoolLogoAssets", fileName);

        var mapped = _mapper.Map<SchoolLogoAsset>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.UploadedDate = DateTime.UtcNow;
        mapped.Path = resultImage;
        await _schoolLogoAssetRepository.InsertAsync(mapped);

        return _mapper.Map<SchoolLogoAssetForResultDto>(mapped);
    }
}
