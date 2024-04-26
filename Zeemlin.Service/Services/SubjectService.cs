using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Lesson;
using Zeemlin.Service.DTOs.Subjects;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class SubjectService : ISubjectService
{
    private readonly IMapper _mapper;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILessonRepository _lessonRepository;

    public SubjectService(
        IMapper mapper,
        ISubjectRepository subjectRepository,
        ILessonRepository lessonRepository)
    {
        _mapper = mapper;
        _subjectRepository = subjectRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<SubjectForResultDto> CreateAsync(SubjectForCreationDto dto)
    {
        var lesson = await _lessonRepository.SelectAll()
            .AsNoTracking()
            .Where(l => l.Id == dto.LessonId)
            .FirstOrDefaultAsync();
        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found");


        var mapped = _mapper.Map<Subject>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await _subjectRepository.InsertAsync(mapped);

        return _mapper.Map<SubjectForResultDto>(mapped);
    }

    public async Task<SubjectForResultDto> ModifyAsync(long id, SubjectForUpdateDto dto)
    {
        var update = await _subjectRepository.SelectAll()
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();

        if (update is null)
            throw new ZeemlinException(404, "Subject Not Found");


        var lesson = await _lessonRepository.SelectAll()
            .AsNoTracking()
            .Where(l => l.Id == dto.LessonId)
            .FirstOrDefaultAsync();
        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found");

        var mappedsubject = _mapper.Map(dto, update);
        mappedsubject.UpdatedAt = DateTime.UtcNow;
        await _subjectRepository.UpdateAsync(mappedsubject);

        return _mapper.Map<SubjectForResultDto>( mappedsubject);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var update = await _subjectRepository.SelectAll()
            .AsNoTracking()
            .Where(n => n.Id == id)
            .FirstOrDefaultAsync();

        if (update is null)
            throw new ZeemlinException(404, "Subject Not Found");

        await _subjectRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<SubjectForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var subjects = await _subjectRepository
            .SelectAll()
            .Include(s => s.Lesson)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        var result = subjects.Select(s =>
        {
            var sdto = _mapper.Map<SubjectForResultDto>(s);
            sdto.Lesson = s.Lesson != null ? _mapper.Map<LessonForResultDto>(s.Lesson) : null;

            return s;
        });

        return _mapper.Map<IEnumerable<SubjectForResultDto>>(subjects);
    }

    public async Task<SubjectForResultDto> RetrieveByIdAsync(long id)
    {
        var subject = await _subjectRepository.SelectAll()
            .Include(s => s.Lesson)
            .Where(n => n.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (subject is null)
            throw new ZeemlinException(404, "Subject Not Found");

        var result = _mapper.Map<SubjectForResultDto>(subject);
        result.Lesson = subject.Lesson != null ? _mapper.Map<LessonForResultDto>(subject.Lesson) : null;

        return result;
    }

    public async Task<IEnumerable<SubjectForResultDto>> RetrieveSubjectsByLessonIdAsync(long lessonId, PaginationParams @params)
    {
        var subjects = await _subjectRepository
            .SelectAll()
            .Include(t => t.Lesson) // Include Lesson for eager loading
            .Where(t => t.LessonId == lessonId)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<SubjectForResultDto>>(subjects);

        return result;
    }

}
