using Microsoft.AspNetCore.Http;

namespace Zeemlin.Service.DTOs.Assets.VideoLessonAssets;

public class VideoLessonAssetForCreationDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile Path { get; set; }
    public long LessonId { get; set; }
}
