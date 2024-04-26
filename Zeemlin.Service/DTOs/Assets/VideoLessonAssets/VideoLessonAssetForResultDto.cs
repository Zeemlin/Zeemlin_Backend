namespace Zeemlin.Service.DTOs.Assets.VideoLessonAssets;

public class VideoLessonAssetForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }
    public string UploadedDate { get; set; }
    public long LessonId { get; set; }
    public string? ContentType { get; set; }
    public long? Size { get; set; }
}
