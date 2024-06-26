﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.DTOs.Lesson;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly ILessonRepository lessonRepository;
    private readonly IGroupRepository groupRepository;
    private readonly ITeacherRepository teacherRepository;

    public LessonService(
        IMapper mapper,
        ILessonRepository lessonRepository,
        IGroupRepository groupRepository,
        ITeacherRepository teacherRepository)
    {
        this._mapper = mapper;
        this.lessonRepository = lessonRepository;
        this.groupRepository = groupRepository;
        this.teacherRepository = teacherRepository;
    }


    public async Task<LessonForResultDto> CreateAsync(LessonForCreationDto dto)
    {
        var group = await groupRepository.SelectAll()
            .Where(g => g.Id == dto.GroupId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (group is null)
            throw new ZeemlinException(404, "Group not found");

        var teacher = await teacherRepository.SelectAll()
            .Where(t => t.Id == dto.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (teacher is null)
            throw new ZeemlinException(404, "Teacher not found");

        var existingLesson = await lessonRepository.SelectAll()
            .Where(l => l.Title.ToLower() == dto.Title.ToLower() &&
            l.GroupId == dto.GroupId &&
            l.TeacherId == dto.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (existingLesson is not null)
        {
            // Get teacher name for exception message
            var teacherName = teacher?.FirstName + " " + teacher?.LastName; // Handle null teacher

            throw new ZeemlinException(409, $"Lesson with this name already exists in the group. {teacherName} created a lesson with this title on {existingLesson.CreatedAt:yyyy-MM-dd}");
        }

        var mappedLesson = _mapper.Map<Lesson>(dto);
        mappedLesson.CreatedAt = DateTime.UtcNow;
        var createLesson = await lessonRepository.InsertAsync(mappedLesson);

        return _mapper.Map<LessonForResultDto>(mappedLesson);

    }

    public async Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto)
    {
        var lessons = await lessonRepository.SelectAll()
            .Where(l => l.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (lessons is null)
            throw new ZeemlinException(404, "Lesson is not found");

        var group = await groupRepository.SelectAll()
            .Where(g => g.Id == dto.GroupId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (group is null)
            throw new ZeemlinException(404, "Group not found");

        var teacher = await teacherRepository.SelectAll()
            .Where(t => t.Id == dto.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (teacher is null)
            throw new ZeemlinException(404, "Teacher not found");

        var existingLesson = await lessonRepository.SelectAll()
            .Where(l => l.Title.ToLower() == dto.Title.ToLower() &&
            l.GroupId == dto.GroupId &&
            l.TeacherId == dto.TeacherId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (existingLesson is not null)
        {
            // Get teacher name for exception message
            var teacherName = teacher?.FirstName + " " + teacher?.LastName; // Handle null teacher

            throw new ZeemlinException(409, $"Lesson with this name already exists in the group. {teacherName} created a lesson with this title on {existingLesson.CreatedAt:yyyy-MM-dd}");
        }

        var lesson = _mapper.Map(dto, lessons);
        lesson.UpdatedAt = DateTime.UtcNow;
        await lessonRepository.UpdateAsync(lesson);

        return _mapper.Map<LessonForResultDto>(lesson);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var lesson = await lessonRepository.SelectAll()
        .Where(l => l.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found");

        await lessonRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync()
    {
        var lessons = await lessonRepository.SelectAll().AsNoTracking().ToListAsync();

        return _mapper.Map<IEnumerable<LessonForResultDto>>(lessons);
    }



    public async Task<LessonForResultDto> RetrieveIdAsync(long id)
    {
        var lesson = await lessonRepository.SelectAll()
        .Where(l => l.Id == id)
        .AsNoTracking()
        .FirstOrDefaultAsync();
        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found");

        return _mapper.Map<LessonForResultDto>(lesson);
    }
}
