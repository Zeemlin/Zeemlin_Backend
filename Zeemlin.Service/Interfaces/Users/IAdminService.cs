using Zeemlin.Domain.Entities.Users;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Users.Admins;

namespace Zeemlin.Service.Interfaces.Users;

public interface IAdminService
{
    Task<bool> RemoveAsync(long id);
    Task<List<Admin>> SearchAdmins(string searchTerm);
    Task<AdminForResultDto> RetrieveByIdAsync(long id);
    Task<AdminForResultDto> CreateAsync(AdminForCreationDto dto);
    Task<AdminForResultDto> ModifyAsync(long id, AdminForUpdateDto dto);
    Task<IEnumerable<AdminForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<AdminForResultDto>> RetrieveBySchoolIdAsync(long schoolId, PaginationParams @params);
}
