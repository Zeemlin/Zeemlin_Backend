using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Users.Parents;

namespace Zeemlin.Service.Interfaces.Users;

public interface IParentService
{
    Task<bool> RemoveAsync(long id);
    Task<ParentForResultDto> RetrieveByIdAsync(long id);
    Task<ParentForResultDto> CreateAsync(ParentForCreationDto dto);
    Task<ParentForResultDto> ModifyAsync(long id, ParentForUpdateDto dto);
    Task<IEnumerable<ParentForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
