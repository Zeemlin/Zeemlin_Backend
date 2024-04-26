using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Data.IRepositries.Users;
using Zeemlin.Domain.Entities.Users;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Schools;
using Zeemlin.Service.DTOs.Users.Admins;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Users;

namespace Zeemlin.Service.Services.Users;

public class AdminService : IAdminService
{
    private readonly IMapper _mapper;
    private readonly IAdminRepository _adminRepository;
    private readonly ISchoolRepository _schoolRepository;
    public AdminService(
        IMapper mapper,
        IAdminRepository adminRepository,
        ISchoolRepository schoolRepository)
    {
        _mapper = mapper;
        _adminRepository = adminRepository;
        _schoolRepository = schoolRepository;
    }

    public async Task<AdminForResultDto> CreateAsync(AdminForCreationDto dto)
    {
        var IsValidUsername = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.Username.ToLower() == dto.Username.ToLower())
            .FirstOrDefaultAsync();

        if (IsValidUsername is not null)
            throw new ZeemlinException(409, "Username already exists");

        var IsValidUserEmail = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.Email.ToLower() == dto.Email.ToLower())
            .FirstOrDefaultAsync();

        if (IsValidUserEmail is not null)
            throw new ZeemlinException(409, "Email already exists");

        var IsValidPassportSeria = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.PassportSeria == dto.PassportSeria)
            .FirstOrDefaultAsync();

        if (IsValidPassportSeria is not null)
            throw new ZeemlinException(409, "PassportSeria already exists");

        var IsValidSchoolNumber = await _schoolRepository.SelectAll()
            .Where(s => s.Id == dto.SchoolId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (IsValidSchoolNumber is null)
            throw new ZeemlinException(404, "School Not Found");


        var mapped = _mapper.Map<Admin>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await _adminRepository.InsertAsync(mapped);

        return _mapper.Map<AdminForResultDto>(mapped);
    }

    public async Task<AdminForResultDto> ModifyAsync(long id, AdminForUpdateDto dto)
    {
        var IsValidId = await _adminRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");

        var IsValidUsername = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.Username.ToLower() == dto.Username.ToLower())
            .FirstOrDefaultAsync();

        if (IsValidUsername is not null)
            throw new ZeemlinException(409, "Username already exists");

        var IsValidUserEmail = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.Email.ToLower() == dto.Email.ToLower())
            .FirstOrDefaultAsync();

        if (IsValidUserEmail is not null)
            throw new ZeemlinException(409, "Email already exists");

        var IsValidPassportSeria = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(u => u.PassportSeria == dto.PassportSeria)
            .FirstOrDefaultAsync();

        if (IsValidPassportSeria is not null)
            throw new ZeemlinException(404, "PassportSeria already exists");

        var IsValidSchoolNumber = await _adminRepository
            .SelectAll()
            .AsNoTracking()
            .Where(s => s.SchoolId == dto.SchoolId)
            .FirstOrDefaultAsync();

        if (IsValidSchoolNumber is null)
            throw new ZeemlinException(404, "School Not Found");

        var mapped = _mapper.Map(dto, IsValidId);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _adminRepository.UpdateAsync(mapped);

        return _mapper.Map<AdminForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var IsValidId = await _adminRepository
            .SelectAll().AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");

        await _adminRepository.DeleteAsync(id);
        return true;
    }

    public async Task<List<Admin>> SearchAdmins(string searchTerm)
    {
        var query = _adminRepository.SelectAll().Where(a =>
            a.Username.Contains(searchTerm) ||
            a.PassportSeria.Contains(searchTerm) ||
            a.Email.Contains(searchTerm));
        return await query.ToListAsync();
    }




    public async Task<IEnumerable<AdminForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var SuperAdmins = await _adminRepository.SelectAll().AsNoTracking().ToPagedList(@params).ToListAsync();

        return _mapper.Map<IEnumerable<AdminForResultDto>>(SuperAdmins);
    }

    public async Task<AdminForResultDto> RetrieveByIdAsync(long id)
    {
        var IsValidId = await _adminRepository
            .SelectAll()
            .Include(s => s.School)
            .AsNoTracking()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (IsValidId is null)
            throw new ZeemlinException(404, "Not Found");

        var result = _mapper.Map<AdminForResultDto>(IsValidId);
        result.SchoolForResultDto = IsValidId.School != null ? _mapper.Map<SchoolForResultDto>(IsValidId.School) : null;

        return result;
    }

    public async Task<IEnumerable<AdminForResultDto>> RetrieveBySchoolIdAsync(long schoolId, PaginationParams @params)
    {
        var admins = await _adminRepository.SelectAll()
          .Where(a => a.SchoolId == schoolId)
          .AsNoTracking()
          .ToPagedList(@params)
          .ToListAsync();

        return _mapper.Map<IEnumerable<AdminForResultDto>>(admins);
    }
}
