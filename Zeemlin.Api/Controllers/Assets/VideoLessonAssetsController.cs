using Microsoft.AspNetCore.Mvc;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Assets.VideoLessonAssets;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Assets;

namespace Zeemlin.Api.Controllers.Assets
{
    public class VideoLessonAssetsController : BaseController
    {
        private readonly IVideoLessonAssetService _videoLessonAssetService;

        public VideoLessonAssetsController(IVideoLessonAssetService videoLessonAssetService)
        {
            _videoLessonAssetService = videoLessonAssetService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] VideoLessonAssetForCreationDto dto)
            => Ok(await _videoLessonAssetService.UploadAsync(dto));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoLessonAssetForResultDto>>> GetAll([FromQuery] PaginationParams @params)
        {
            var videoLessons = await _videoLessonAssetService.RetrieveAllAsync(@params);
            return Ok(videoLessons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoLessonAssetForResultDto>> GetById(long id)
        {
            var videoLesson = await _videoLessonAssetService.RetrieveByIdAsync(id);
            return Ok(videoLesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] VideoLessonAssetForUpdateDto dto)
        => Ok(await _videoLessonAssetService.ModifyAsync(id, dto));


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await _videoLessonAssetService.DeletePictureAsync(id));

        [HttpGet("by-lesson/{lessonId}")]
        public async Task<ActionResult<IEnumerable<VideoLessonAssetForResultDto>>> GetVideoLessonsByLessonIdAsync(long lessonId, [FromQuery] PaginationParams @params)
        {
            var videoLessons = await _videoLessonAssetService.GetVideoLessonsByLessonIdAsync(lessonId, @params);
            return Ok(videoLessons);
        }
    }
}
