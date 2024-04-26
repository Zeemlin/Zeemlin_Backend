using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.DTOs.TeacherGroups;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class TeacherGroupService : ITeacherGroupService
{
    private readonly IMapper _mapper;
    private readonly ITeacherGroupRepository _repository;
    private readonly ITeacherRepository _teacher;
    private readonly IGroupRepository _groupRepository;

    public TeacherGroupService(
        IMapper mapper,
        ITeacherGroupRepository repository,
        ITeacherRepository teacher,
        IGroupRepository groupRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _teacher = teacher;
        _groupRepository = groupRepository;
    }

    public async Task<TeacherGroupForResultDto> AddAsync(TeacherGroupForCreationDto dto)
    {
        var group = await _groupRepository.SelectAll()
            .Where(u => u.Id == dto.GroupId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (group is null)
            throw new ZeemlinException(404, "Group not found");

        var teacher = await _teacher.SelectAll()
            .Where(s => s.Id == dto.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (teacher is null)
            throw new ZeemlinException(404, "Teacher not found");

        // Check for existing teacher-group relationship
        var teacherGroup = await _repository.SelectAll()
            .Where(tg => tg.TeacherId == dto.TeacherId &&
                        tg.GroupId == dto.GroupId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (teacherGroup is not null)
            throw new ZeemlinException(409, $"{teacher.FirstName} {teacher.LastName} is already in this {group.Name} group");

        // Check for existing MainTeacher in the group
        var existingMainTeacher = await _repository.SelectAll()
    .Where(tg => tg.GroupId == dto.GroupId && tg.Role == TeacherRole.MainTeacher)
    .AsNoTracking()
    .FirstOrDefaultAsync();

        if (existingMainTeacher is not null && dto.Role == TeacherRole.MainTeacher)
        {
            throw new ZeemlinException(409, "Main teacher is already assigned to this group.");
        }

        // Assign MainTeacher role if necessary
        dto.Role = existingMainTeacher is null || dto.Role == TeacherRole.MainTeacher ? TeacherRole.MainTeacher : dto.Role;

        var mappedTeacherGroup = _mapper.Map<TeacherGroup>(dto);
        mappedTeacherGroup.CreatedAt = DateTime.UtcNow;
        await _repository.InsertAsync(mappedTeacherGroup);

        return _mapper.Map<TeacherGroupForResultDto>(mappedTeacherGroup);
    }


    public async Task<TeacherGroupForResultDto> ModifyAsync(long id, TeacherGroupForUpdateDto dto)
    {
        var isvalidGroup = await _repository.SelectAll()
        .Where(u => u.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (isvalidGroup is null)
            throw new ZeemlinException(404, "Not found");

        var group = await _groupRepository.SelectAll()
        .Where(u => u.Id == dto.GroupId)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (group is null)
            throw new ZeemlinException(404, "Group not found");

        var teacher = await _teacher.SelectAll()
        .Where(s => s.Id == dto.TeacherId)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (teacher is null)
            throw new ZeemlinException(404, "Teacher not found");

        var teacherGroup = await _repository.SelectAll()
            .Where(tg => tg.TeacherId == dto.TeacherId &&
            tg.GroupId == dto.GroupId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (teacherGroup is not null)
            throw new ZeemlinException(409, $"{teacher.FirstName} {teacher.LastName} is available in this {group.Name} group");

        var existingMainTeacher = await _repository.SelectAll()
            .Where(tg => tg.GroupId == dto.GroupId && tg.Role == TeacherRole.MainTeacher)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (existingMainTeacher is not null)
            throw new ZeemlinException(409, "Main teacher is ready for this group");

        var mappedTeacherGroup = _mapper.Map(dto, isvalidGroup);
        mappedTeacherGroup.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(mappedTeacherGroup);

        return _mapper.Map<TeacherGroupForResultDto>(mappedTeacherGroup);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var group = await _repository.SelectAll()
        .Where(u => u.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (group is null)
            throw new ZeemlinException(404, "Not found");

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<TeacherGroupForResultDto>> RetrieveAllAsync()
    {
        var users = await _repository.SelectAll().ToListAsync();

        return _mapper.Map<IEnumerable<TeacherGroupForResultDto>>(users);
    }

    public async Task<TeacherGroupForResultDto> RetrieveByIdAsync(long id)
    {
        var group = await _repository.SelectAll()
        .Where(u => u.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (group is null)
            throw new ZeemlinException(404, "Not found");

        return _mapper.Map<TeacherGroupForResultDto>(group);
    }
}
