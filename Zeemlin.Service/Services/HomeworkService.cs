using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zeemlin.Data.IRepositries;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.Commons.Extentions;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Assets.HomeworkAssets;
using Zeemlin.Service.DTOs.Homework;
using Zeemlin.Service.DTOs.Lesson;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Service.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IMapper _mapper;
    private readonly IHomeworkRepository _homeworkRepository;
    private readonly ILessonRepository _lessonRepository;

    public HomeworkService(IMapper mapper,
        IHomeworkRepository homeworkRepository,
        ILessonRepository lessonRepository)
    {
        _mapper = mapper;
        _homeworkRepository = homeworkRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<HomeworkForResultDto> CreateAsync(HomeworkForCreationDto dto)
    {
        var lesson = await _lessonRepository.SelectAll()
            .Where(h => h.Id == dto.LessonId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found.");

        if (dto.Deadline < DateTime.Now)
        {
            throw new ZeemlinException(400, "Invalid date entered. DueTime cannot be in the past.");
        }

        var mappedHomework = _mapper.Map<Homework>(dto);
        mappedHomework.CreatedAt = DateTime.UtcNow;
        await _homeworkRepository.InsertAsync(mappedHomework);

        return _mapper.Map<HomeworkForResultDto>(mappedHomework);
    }

    public async Task<HomeworkForResultDto> ModifyAsync(long id, HomeworkForUpdateDto dto)
    {
        var homework = await _homeworkRepository.SelectAll()
            .Where(h => h.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (homework is null)
            throw new ZeemlinException(404, "Homework not found");

        var lesson = await _lessonRepository.SelectAll()
            .Where(h => h.Id == dto.LessonId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found.");

        if (dto.Deadline < DateTime.Now)
            throw new ZeemlinException(400, "Invalid date entered. DueTime cannot be in the past.");

        homework.UpdatedAt = DateTime.UtcNow;
        var vazifa = _mapper.Map(dto, homework);
        await _homeworkRepository.UpdateAsync(vazifa);

        return _mapper.Map<HomeworkForResultDto>(vazifa);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var homework = await _homeworkRepository.SelectAll()
            .Where(h => h.Id == id)
            .FirstOrDefaultAsync();
        if (homework is null)
            throw new ZeemlinException(404, "Homework not found");

        await _homeworkRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<HomeworkForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var homeworks = await _homeworkRepository.SelectAll()
            .Include(h => h.Lesson)
            .Include(h => h.Assets)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        var result = homeworks.Select(h =>
        {
            var hdto = _mapper.Map<HomeworkForResultDto>(h);
            hdto.Lesson = _mapper.Map<LessonForResultDto>(h.Lesson);
            hdto.Asset = h.Assets != null
        ? _mapper.Map<ICollection<HomeworkAssetForResultDto>>(h.Assets)
        : null;

            return hdto;
        });

        return result;
    }

    public async Task<HomeworkForResultDto> RetrieveIdAsync(long id)
    {
        var homework = await _homeworkRepository.SelectAll()
            .Include(h => h.Lesson)
            .Include(h => h.Assets)// Then include only the Path property
            .Where(h => h.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (homework is null)
            throw new ZeemlinException(404, "Homework not found");

        var hdto = _mapper.Map<HomeworkForResultDto>(homework);

        hdto.Lesson = _mapper.Map<LessonForResultDto>(homework.Lesson);
        hdto.Asset = homework.Assets != null
        ? _mapper.Map<ICollection<HomeworkAssetForResultDto>>(homework.Assets)
        : null;

        return hdto;
    }

    public async Task<IEnumerable<HomeworkForResultDto>> RetrieveByLessonIdAsync(long lessonId, PaginationParams @params)
    {
        var lesson = await _lessonRepository.SelectAll()
            .Where(h => h.Id == lessonId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (lesson is null)
            throw new ZeemlinException(404, "Lesson not found.");

        var homeworks = await _homeworkRepository.SelectAll()
          .Include(h => h.Lesson)
          .Include(h => h.Assets)
          .Where(h => h.LessonId == lessonId)
          .AsNoTracking()
          .ToPagedList(@params)
          .ToListAsync();

        return _mapper.Map<IEnumerable<HomeworkForResultDto>>(homeworks);
    }
}
