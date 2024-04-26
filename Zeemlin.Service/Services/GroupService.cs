using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Group;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class GroupService : IGroupService
{
    private readonly IMapper _mapper;
    private readonly IGroupRepository _groupRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISchoolRepository _schoolRepository;

    public GroupService(
        IGroupRepository repository,
        IMapper mapper,
        ICourseRepository courseRepository,
        ISchoolRepository schoolRepository)
    {
        _mapper = mapper;
        _groupRepository = repository;
        _courseRepository = courseRepository;
        _schoolRepository = schoolRepository;
    }

    public async Task<GroupForResultDto> CreateAsync(GroupForCreationDto dto)
    {
        var course = await _courseRepository.SelectAll()
            .Where(c => c.Id == dto.CourseId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (course is null)
            throw new ZeemlinException(404, "Course not found");

        var groupName = await _groupRepository.SelectAll()
            .Where(gn => gn.CourseId == dto.CourseId 
            && gn.Name.ToLower() == dto.Name.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (groupName is not null)
            throw new ZeemlinException(409, $"Group with same name already exists in this {course.Name}");

        var mappedGroup = _mapper.Map<Group>(dto);
        mappedGroup.CreatedAt = DateTime.UtcNow;
        var createdGroup = await _groupRepository.InsertAsync(mappedGroup);

        return _mapper.Map<GroupForResultDto>(createdGroup);
    }

    public async Task<GroupForResultDto> ModifyAsync(long id, GroupForUpdateDto dto)
    {
        var group = await _groupRepository.SelectAll()
            .AsNoTracking()
            .Where (g => g.Id == id)
            .FirstOrDefaultAsync();
        
        if (group is null)
            throw new ZeemlinException(404, "Group Not Found");

        var course = await _courseRepository.SelectAll()
            .Where(c => c.Id == dto.CourseId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (course is null)
            throw new ZeemlinException(404, "Course not found");

        var groupNameUpdate = await _groupRepository.SelectAll()
            .Where(gn => gn.CourseId == dto.CourseId
            && gn.Name.ToLower() == dto.Name.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (groupNameUpdate is not null)
            throw new ZeemlinException(409, $"Group with same name already exists in this {course.Name}");

        group.UpdatedAt = DateTime.UtcNow;
        var groups = _mapper.Map(dto,group);
        await _groupRepository.UpdateAsync(groups);
        
        return _mapper.Map<GroupForResultDto>(groups);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var group = await _groupRepository.SelectAll()
            .Where (g => g.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (group is null)
            throw new ZeemlinException(404, "Group Not Found");

        await _groupRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<GroupForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var group = await _groupRepository.SelectAll().AsNoTracking().ToPagedList(@params).ToListAsync();

        return _mapper.Map<IEnumerable<GroupForResultDto>>(group);
    }

    public async Task<GroupForResultDto> RetrieveByIdAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Group ID must be a positive value.");
        }

        var group = await _groupRepository.SelectAll()
            .Include(g => g.TeacherGroups)
                .ThenInclude(tg => tg.Teacher) 
            .Include(g => g.Course) 
            .Include(g => g.StudentGroups) 
            .Where(g => g.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (group is null)
            throw new ZeemlinException(404, "Group Not Found");

        var groupDto = _mapper.Map<GroupForResultDto>(group);
        var mainTeacher = group.TeacherGroups.FirstOrDefault(tg => tg.Role == TeacherRole.MainTeacher);

        groupDto.TeacherFirstName = mainTeacher?.Teacher?.FirstName;
        groupDto.TeacherLastName = mainTeacher?.Teacher?.LastName;

        groupDto.GroupData = group.TeacherGroups.Select(tg => new GroupDataResultDto
        {
            TeacherFirstName = tg.Teacher?.FirstName,
            TeacherLastName = tg.Teacher?.LastName,
            ScienceType = tg.Teacher?.ScienceType.ToString(),
        }).ToList();

        groupDto.TotalTeacherCount = group.TeacherGroups.Count();
        groupDto.StudentCount = group.StudentGroups.Count();

        return groupDto;
    }




    public async Task<IEnumerable<SearchGroupResultDto>> SearchGroupsAsync(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            throw new ArgumentException("Search term cannot be empty.");
        }

        var groups = await _groupRepository.SelectAll()
            .Include(g => g.TeacherGroups)
                .ThenInclude(tg => tg.Teacher)
            .Include(g => g.Course)
            .Include(g => g.StudentGroups)
            .Where(g => g.Name.Contains(searchTerm) || g.Course.Name.Contains(searchTerm))
            .AsNoTracking()
            .ToListAsync();

        var groupDtos = groups.Select(group =>
        {
            var groupDto = new SearchGroupResultDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CourseId = group.CourseId,
                CourseName = group.Course.Name,
                TotalTeacherCount = group.TeacherGroups.Count(),
                StudentCount = group.StudentGroups.Count()
            };

            var mainTeacher = group.TeacherGroups.FirstOrDefault(tg => tg.Role == TeacherRole.MainTeacher);

            groupDto.TeacherFirstName = mainTeacher?.Teacher?.FirstName;
            groupDto.TeacherLastName = mainTeacher?.Teacher?.LastName;

            groupDto.GroupData = group.TeacherGroups.Select(tg => new GroupDataResultDto
            {
                TeacherFirstName = tg.Teacher?.FirstName,
                TeacherLastName = tg.Teacher?.LastName,
                ScienceType = tg.Teacher?.ScienceType.ToString(),
            }).ToList();

            return groupDto;
        }).ToList();

        return groupDtos;
    }


    public async Task<IEnumerable<SearchGroupResultDto>> SearchGroupsBySchoolIdAsync(string searchTerm, long schoolId)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            throw new ArgumentException("Search term cannot be empty.");
        }

        if (schoolId <= 0)
        {
            throw new ArgumentException("School ID must be a positive value.");
        }

        var school = await _schoolRepository.SelectAll()
            .Where(s => s.Id == schoolId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (school is null)
        {
            throw new ZeemlinException(404, "School not found");
        }

        var groups = await _groupRepository.SelectAll()
            .Include(g => g.TeacherGroups)
                .ThenInclude(tg => tg.Teacher)
            .Include(g => g.Course)
            .Include(g => g.StudentGroups)
            .Where(g => g.Name.Contains(searchTerm) 
            && g.Course.SchoolId == schoolId)
            .AsNoTracking()
            .ToListAsync();

        var groupDtos = groups.Select(group =>
        {
            var groupDto = new SearchGroupResultDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CourseId = group.CourseId,
                CourseName = group.Course.Name,
                TotalTeacherCount = group.TeacherGroups.Count(),
                StudentCount = group.StudentGroups.Count()
            };

            var mainTeacher = group.TeacherGroups.FirstOrDefault(tg => tg.Role == TeacherRole.MainTeacher);

            groupDto.TeacherFirstName = mainTeacher?.Teacher?.FirstName;
            groupDto.TeacherLastName = mainTeacher?.Teacher?.LastName;

            groupDto.GroupData = group.TeacherGroups.Select(tg => new GroupDataResultDto
            {
                TeacherFirstName = tg.Teacher?.FirstName,
                TeacherLastName = tg.Teacher?.LastName,
                ScienceType = tg.Teacher?.ScienceType.ToString(),
            }).ToList();

            return groupDto;
        }).ToList();

        return groupDtos;
    }


    public async Task<IEnumerable<GroupForResultDto>> RetrieveGroupsBySchoolIdAsync(long schoolId)
    {
        if (schoolId <= 0)
        {
            throw new ArgumentException("School ID must be a positive value.");
        }

        var school = await _schoolRepository.SelectAll()
            .Where(s => s.Id == schoolId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (school is null)
        {
            throw new ZeemlinException(404, "School not found");
        }

        var groups = await _groupRepository.SelectAll()
            .Include(g => g.TeacherGroups)
                .ThenInclude(tg => tg.Teacher) 
            .Include(g => g.Course) 
            .Include(g => g.StudentGroups)
            .Where(g => g.Course.SchoolId == schoolId)
            .AsNoTracking()
            .ToListAsync();

        var groupDtos = groups.Select(group =>
        {
            var groupDto = _mapper.Map<GroupForResultDto>(group);
            var mainTeacher = group.TeacherGroups.FirstOrDefault(tg => tg.Role == TeacherRole.MainTeacher);

            groupDto.TeacherFirstName = mainTeacher?.Teacher?.FirstName;
            groupDto.TeacherLastName = mainTeacher?.Teacher?.LastName;

            groupDto.GroupData = group.TeacherGroups.Select(tg => new GroupDataResultDto
            {
                TeacherFirstName = tg.Teacher?.FirstName,
                TeacherLastName = tg.Teacher?.LastName,
                ScienceType = tg.Teacher?.ScienceType.ToString(), 
            }).ToList();

            groupDto.TotalTeacherCount = group.TeacherGroups.Count();
            groupDto.StudentCount = group.StudentGroups.Count();

            return groupDto;
        }).ToList();

        return groupDtos;
    }

}
