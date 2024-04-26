using Microsoft.AspNetCore.Mvc;
using Zeemlin.Domain.Enums;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Users.Teachers;
using Zeemlin.Service.Interfaces.Users;

namespace Zeemlin.Api.Controllers.Users;

//[Authorize]
public class TeachersController : BaseController
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TeacherForCreationDto dto)
        => Ok(await _teacherService.CreateAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await this._teacherService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._teacherService.RetrieveByIdAsync(id));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._teacherService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] TeacherForUpdateDto dto)
        => Ok(await this._teacherService.ModifyAsync(id, dto));

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(string searchTerm, long currentSchoolId)
    {
        var searchResults = await _teacherService.SearchAsync(searchTerm, currentSchoolId);
        return Ok(searchResults);
    }

    [HttpGet("search/superadmin")]
    public async Task<IActionResult> SearchByPhoneOrEmailForSuperAdminsAsync(string searchTerm)
    {
        var searchResults = await _teacherService.SearchByPhoneOrEmailForSuperAdminsAsync(searchTerm);
        return Ok(searchResults);
    }

    [HttpGet("filtered")]
    public async Task<IActionResult> GetFilteredTeachers(
        ScienceType scienceType, // Optional query parameter
        long schoolId = 0) // Optional query parameter with default value
    {
        var filteredTeachers = await _teacherService.GetFilteredTeachers(
            scienceType,  // Convert to enum if valid
            schoolId);
        return Ok(filteredTeachers);
       
    }

    [HttpGet("schools/{schoolId}/teachers")]
    public async Task<IActionResult> GetTeachersBySchoolAsync([FromRoute(Name = "schoolId")] long schoolId)
    {
        var teachers = await _teacherService.GetTeachersBySchoolAsync(schoolId);
        return Ok(teachers);
    }

}
