using System.Text.Json.Serialization;
using Zeemlin.Service.DTOs.Events;

namespace Zeemlin.Service.DTOs.Assets.EventAssets;

public class EventAssetForResultDto
{
    public long Id { get; set; }
    public string Path { get; set; }
    public DateTime UploadedDate { get; set; }
    public long EventId { get; set; }
    [JsonIgnore]
    public EventForResultDto EventForResultDto { get; set; }

}
