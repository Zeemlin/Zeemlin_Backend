using Zeemlin.Domain.Commons;

namespace Zeemlin.Domain.Entities.Assets;

public class VideoLessonAsset : Auditable
{
    public string Title { get; set; } 
    public string Description { get; set; }
    public string Path { get; set; }
    public DateTime UploadedDate { get; set; }
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public string? ContentType { get; set; }
    public long? Size { get; set; }

}
