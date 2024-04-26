using Microsoft.AspNetCore.Mvc;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Group;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Api.Controllers
{
    public class GroupsController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] GroupForCreationDto dto)
        => Ok(await this._groupService.CreateAsync(dto));

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await this._groupService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._groupService.RetrieveByIdAsync(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._groupService.RemoveAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] GroupForUpdateDto dto)
        => Ok(await this._groupService.ModifyAsync(id, dto));

        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchAsync(string searchTerm)
        {
            var groups = await _groupService.SearchGroupsAsync(searchTerm);
            return Ok(groups);
        }

        [HttpGet("search-schoolId")]
        public async Task<IActionResult> SearchBySchoolIdAsync(string searchTerm, long schoolId)
        {
            var groups = await _groupService.SearchGroupsBySchoolIdAsync(searchTerm, schoolId);
            return Ok(groups);
        }

        [HttpGet("{schoolId}/groups")]
        public async Task<IActionResult> RetrieveGroupsBySchoolIdAsync(long schoolId)
        {
            var groups = await _groupService.RetrieveGroupsBySchoolIdAsync(schoolId);
            return Ok(groups);
        }

    }
}
