using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Group;

namespace Zeemlin.Service.Interfaces;

public interface IGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<GroupForResultDto> RetrieveByIdAsync(long id);
    Task<GroupForResultDto> CreateAsync(GroupForCreationDto dto);
    Task<GroupForResultDto> ModifyAsync(long id, GroupForUpdateDto dto);
    Task<IEnumerable<SearchGroupResultDto>> SearchGroupsAsync(string searchTerm);
    Task<IEnumerable<GroupForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<GroupForResultDto>> RetrieveGroupsBySchoolIdAsync(long schoolId);
    Task<IEnumerable<SearchGroupResultDto>> SearchGroupsBySchoolIdAsync(string searchTerm, long schoolId);

}
