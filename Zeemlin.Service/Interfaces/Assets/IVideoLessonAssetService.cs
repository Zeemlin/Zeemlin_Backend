using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Assets.VideoLessonAssets;

namespace Zeemlin.Service.Interfaces.Assets;

public interface IVideoLessonAssetService
{
    Task<bool> DeletePictureAsync(long VideoLessonId);
    Task<VideoLessonAssetForResultDto> RetrieveByIdAsync(long id);
    Task<VideoLessonAssetForResultDto> UploadAsync(VideoLessonAssetForCreationDto dto);
    Task<VideoLessonAssetForResultDto> ModifyAsync(long id, VideoLessonAssetForUpdateDto dto);
    Task<IEnumerable<VideoLessonAssetForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<VideoLessonAssetForResultDto>> GetVideoLessonsByLessonIdAsync(long lessonId, PaginationParams @params);
}
