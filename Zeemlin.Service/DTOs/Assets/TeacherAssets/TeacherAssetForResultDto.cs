using System.Text.Json.Serialization;
using Zeemlin.Service.DTOs.Users.Teachers;

namespace Zeemlin.Service.DTOs.Assets.TeacherAssets;

public class TeacherAssetForResultDto
{
    public long Id { get; set; }
    public long TeacherId { get; set; }
    public string Path { get; set; }
    public DateTime UploadedDate { get; set; }
    [JsonIgnore]
    public TeacherForResultDto Teacher { get; set; }
}
