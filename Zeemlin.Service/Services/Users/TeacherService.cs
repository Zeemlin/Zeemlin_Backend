using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities.Users;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Assets.TeacherAssets;
using Zeemlin.Service.DTOs.Group;
using Zeemlin.Service.DTOs.TeacherGroups;
using Zeemlin.Service.DTOs.Users.Teachers;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Users;

namespace Zeemlin.Service.Services.Users;

public class TeacherService : ITeacherService
{
    private readonly IMapper _mapper;
    private readonly ITeacherRepository _repository;
    private readonly ISchoolRepository _schoolRepository;

    public TeacherService(
        IMapper mapper,
        ITeacherRepository repository,
        ISchoolRepository schoolRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _schoolRepository = schoolRepository;
    }

    public async Task<TeacherForResultDto> CreateAsync(TeacherForCreationDto dto)
    {
        var TeacherEmailExist = await _repository.SelectAll()
            .AsNoTracking()
            .Where(t => t.Email.ToLower() == dto.Email.ToLower()
            || t.PhoneNumber == dto.PhoneNumber)
            .FirstOrDefaultAsync();

        if (TeacherEmailExist is not null)
            throw new ZeemlinException
                (409, "Teacher is already exist.");

        var mapped = _mapper.Map<Teacher>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var created = await _repository.InsertAsync(mapped);

        return _mapper.Map<TeacherForResultDto>(created);

    }

    public async Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto)
    {
        var Teacher = await _repository
            .SelectAll()
            .AsNoTracking()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        if (Teacher is null)
            throw new ZeemlinException(404, "Teacher is not found.");

        var TeacherEmailExist = await _repository.SelectAll()
            .AsNoTracking()
            .Where(t => t.Email.ToLower() == dto.Email.ToLower()
            || t.PhoneNumber == dto.PhoneNumber)
            .FirstOrDefaultAsync();


        if (TeacherEmailExist is not null)
            throw new ZeemlinException(409, "Teacher is already exist.");

        Teacher.UpdatedAt = DateTime.UtcNow;
        var person = _mapper.Map(dto, Teacher);
        await _repository.UpdateAsync(person);

        return _mapper.Map<TeacherForResultDto>(person);

    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await _repository
            .SelectAll()
            .AsNoTracking()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new ZeemlinException(404, "Teacher is not found.");

        await _repository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var users = await _repository.SelectAll().AsNoTracking().ToPagedList(@params).ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(users);

    }

    public async Task<TeacherForResultDto> RetrieveByIdAsync(long id)
    {
        var query = _repository.SelectAll()
          .Include(t => t.TeacherAsset) // Include TeacherAsset
          .Include(t => t.TeacherGroups) // Include TeacherGroups (eager loading)
          .ThenInclude(tg => tg.Group) // Include Group within TeacherGroup
          .AsNoTracking()
          .Where(t => t.Id == id);

        var user = await query.FirstOrDefaultAsync();

        if (user is null)
            throw new ZeemlinException(404, "Teacher is not found.");

        // Map to TeacherForResultDto
        var teacherDto = _mapper.Map<TeacherForResultDto>(user);
        teacherDto.TeacherAssetForResultDto = user.TeacherAsset != null
          ? _mapper.Map<TeacherAssetForResultDto>(user.TeacherAsset)
          : null;

        // Project TeacherGroups directly (assuming no custom mapping)
        teacherDto.TeacherGroupForResult = user.TeacherGroups.Select(tg => new TeacherGroupForResultDto
        {
            TeacherForResultDto = teacherDto, // Reference the parent teacher
            GroupForResultDto = _mapper.Map<GroupForResultDto>(tg.Group), // Use existing mapping
            Role = tg.Role.ToString()
        }).ToList();

        return teacherDto;
    }

    public async Task<IEnumerable<TeacherSearchResultDto>> SearchAsync(string searchTerm, long currentSchoolId)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return Enumerable.Empty<TeacherSearchResultDto>(); // Handle empty search
        }

        var query = _repository.SelectAll()
        .Include(t => t.TeacherAsset)
        .AsNoTracking();

        // Rewrite query to use LIKE for case-insensitive comparison
        query = query.Where(t =>
           t.PhoneNumber.ToLower().Contains(searchTerm.ToLower())
           ||
           t.Email.ToLower().Contains(searchTerm.ToLower())
        );

        // Filter by teachers associated with the current school
        query = query.Where(t => t.TeacherGroups.Any(tg => tg.Group.Course.SchoolId == currentSchoolId));

        var teachers = await query.ToListAsync();

        // Handle case where no teacher is found within the school
        if (!teachers.Any())
        {
            throw new ZeemlinException(404, "Teacher not found in this school."); // Custom exception
        }

        return teachers.Select(t => new TeacherSearchResultDto
        {
            Id = t.Id,
            FirstName = t.FirstName,
            LastName = t.LastName,
            DateOfBirth = t.DateOfBirth, // Include if necessary
            PhoneNumber = t.PhoneNumber,
            Email = t.Email,
            Biography = t.Biography,
            DistrictName = t.DistrictName,
            ScienceType = t.ScienceType.ToString(),
            TeacherAssetForResultDto = t.TeacherAsset != null
            ? new TeacherAssetForResultDto // Map only if TeacherAsset exists
            {
                Id = t.TeacherAsset.Id,
                TeacherId = t.TeacherAsset.TeacherId,
                Path = t.TeacherAsset.Path,
                UploadedDate = t.TeacherAsset.UploadedDate
            }
            : null // Set TeacherAssetForResultDto to null if not found
        });
    }


    public async Task<IEnumerable<TeacherSearchForSuperAdmin>> SearchByPhoneOrEmailForSuperAdminsAsync(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return Enumerable.Empty<TeacherSearchForSuperAdmin>(); // Handle empty search
        }

        var query = _repository.SelectAll()
          .Include(t => t.TeacherAsset)
          .Include(t => t.TeacherGroups) // Include TeacherGroups
          .Where(t =>
            t.PhoneNumber != null && t.PhoneNumber.ToLower().Contains(searchTerm.ToLower()) ||
            t.Email != null && t.Email.ToLower().Contains(searchTerm.ToLower())
          )
          .AsNoTracking();

        var teachers = await query.ToListAsync();

        return teachers.Select(t => new TeacherSearchForSuperAdmin
        {
            Id = t.Id,
            FirstName = t.FirstName,
            LastName = t.LastName,
            DateOfBirth = t.DateOfBirth, // Include if necessary (consider privacy implications)
            PhoneNumber = t.PhoneNumber,
            Email = t.Email,
            Biography = t.Biography,
            DistrictName = t.DistrictName,
            ScienceType = t.ScienceType.ToString(),
            genderType = t.genderType.ToString(), // Assuming you have a genderType property
            CreatedAt = t.CreatedAt,
            TotalGroupCount = t.TeacherGroups.Count(), // Calculate total group count
            TeacherAssetForResultDto = t.TeacherAsset != null
            ? new TeacherAssetForResultDto // Map only if TeacherAsset exists
            {
                Id = t.TeacherAsset.Id,
                TeacherId = t.TeacherAsset.TeacherId,
                Path = t.TeacherAsset.Path,
                UploadedDate = t.TeacherAsset.UploadedDate
            }
            : null,
            TeacherGroupForResult = t.TeacherGroups.Select(tg => new TeacherGroupForResultDto // Only include Id and Role
            {
                Id = tg.Id,
                Role = tg.Role.ToString(),
            })
          .ToList()
        });
    }

    public async Task<IEnumerable<FilteredTeacherDTO>> GetFilteredTeachers(
    ScienceType? scienceType = null,
    long schoolId = 0) // Mandatory parameter
    {
        if (schoolId <= 0)
        {
            throw new ArgumentException("School ID must be a positive value.");
        }

        var query = _repository.SelectAll()
            .Include(t => t.TeacherAsset) // Eager loading for TeacherAsset
            .AsNoTracking();

        // Apply filters based on input parameters
        query = query.Where(t => t.TeacherGroups.Any(g => g.Group.Course.SchoolId == schoolId));

        // Filter by scienceType (if provided)
        if (scienceType.HasValue)
        {
            query = query.Where(t => t.ScienceType == scienceType.Value);
        }

        // Map teachers to FilteredTeacherDTO
        var filteredTeachers = await query.Select(teacher => new FilteredTeacherDTO
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            DateOfBirth = teacher.DateOfBirth, // Consider formatting or omitting based on needs
            PhoneNumber = teacher.PhoneNumber,
            Email = teacher.Email,
            DistrictName = teacher.DistrictName,
            ScienceType = teacher.ScienceType.ToString(),
            TeacherAssetForResultDto = teacher.TeacherAsset != null ? new TeacherAssetForResultDto // Map teacher asset if it exists
            {
                Path = teacher.TeacherAsset.Path,
                UploadedDate = teacher.TeacherAsset.UploadedDate
            } : null
        }).ToListAsync();

        return filteredTeachers;
    }


    public async Task<IEnumerable<TeacherForResultDto>> GetTeachersBySchoolAsync(long schoolId)
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

        var teachers = await _repository.SelectAll()
            .Include(t => t.TeacherAsset) // Eager loading for TeacherAsset
            .Where(t => t.TeacherGroups.Any(tg => tg.Group.Course.SchoolId == schoolId))
            .ToListAsync();

        // Map teachers to TeacherForResultDto and include TeacherAsset data
        return teachers.Select(teacher => new TeacherForResultDto
        {
            Id = teacher.Id,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            DateOfBirth = teacher.DateOfBirth,
            PhoneNumber = teacher.PhoneNumber,
            Email = teacher.Email,
            Biography = teacher.Biography,
            DistrictName = teacher.DistrictName,
            ScienceType = teacher.ScienceType.ToString(),
            // Include TeacherAsset data if it exists
            TeacherAssetForResultDto = teacher.TeacherAsset != null
                ? _mapper.Map<TeacherAssetForResultDto>(teacher.TeacherAsset)
                : null,
            TeacherGroupForResult = null // Not included in this update
        }).ToList();
    }

}
