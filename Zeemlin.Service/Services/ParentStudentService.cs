using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Zeemlin.Data.IRepositries;
using Zeemlin.Data.IRepositries.Users;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.ParentStudents;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class ParentStudentService : IParentStudentService
{
    private readonly IMapper _mapper;
    private readonly IParentRepository _parentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IParentStudentRepository _parentStudentRepository;



    public ParentStudentService(
        IMapper mapper,
        IParentRepository parentRepository,
        IStudentRepository studentRepository,
        IParentStudentRepository parentStudentRepository)
    {
        _mapper = mapper;
        _parentRepository = parentRepository;
        _studentRepository = studentRepository;
        _parentStudentRepository = parentStudentRepository;
    }

    private async Task<bool> ValidateParentStudentAssociation(long parentId, long studentId)
    {
        var parent = await _parentRepository
            .SelectAll()
            .Where(p => p.Id == parentId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (parent is null)
        {
            throw new ZeemlinException(400, "Parent not found.");
        }

        var student = await _studentRepository
            .SelectAll()
            .Where(s => s.Id == studentId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (student is null)
        {
            throw new ZeemlinException(400, "Student not found.");
        }

        // Check for existing association (parent-student combination)
        var existingAssociation = await _parentStudentRepository.SelectAll()
            .Where (p => p.Id == studentId && p.ParentId == parentId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (existingAssociation is not null)
        {
            throw new ZeemlinException(400, "This student is already associated with this parent.");
        }

        return true;
    }

    public async Task<ParentStudentForResultDto> AddAsync(ParentStudentForCreationDto dto)
    {
        await ValidateParentStudentAssociation(dto.ParentId, dto.StudentId);

        var mapped = _mapper.Map<ParentStudent>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await _parentStudentRepository.InsertAsync(mapped);

        return _mapper.Map<ParentStudentForResultDto>(mapped);
    }

    public async Task<ParentStudentForResultDto> ModifyAsync(long id, ParentStudentForUpdateDto dto)
    {
        var isValidId = await _parentStudentRepository.SelectAll()
            .Where(ps => ps.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (isValidId is null)
            throw new ZeemlinException(404, "Not found");

        await ValidateParentStudentAssociation(dto.ParentId, dto.StudentId);

        var mapped = _mapper.Map(dto, isValidId);
        mapped.UpdatedAt = DateTime.UtcNow;
        await _parentStudentRepository.UpdateAsync(mapped);

        return _mapper.Map<ParentStudentForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var isValidId = await _parentStudentRepository.SelectAll()
            .Where(ps => ps.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (isValidId is null)
            throw new ZeemlinException(404, "Not found");

        await _parentStudentRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ParentStudentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var parentStudents = await _parentStudentRepository.SelectAll().AsNoTracking()
            .ToPagedList(@params).ToListAsync();

        return _mapper.Map<IEnumerable<ParentStudentForResultDto>>(parentStudents);
    }

    public async Task<ParentStudentForResultDto> RetrieveByIdAsync(long id)
    {
        var isValidId = await _parentStudentRepository.SelectAll()
            .Where(ps => ps.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (isValidId is null)
            throw new ZeemlinException(404, "Not found");

        return _mapper.Map<ParentStudentForResultDto>(isValidId);
    }
}
