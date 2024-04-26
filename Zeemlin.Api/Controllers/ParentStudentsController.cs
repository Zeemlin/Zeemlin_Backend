using Microsoft.AspNetCore.Mvc;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.ParentStudents;
using Zeemlin.Service.DTOs.StudentGroups;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces;
using Zeemlin.Service.Services;

namespace Zeemlin.Api.Controllers;

public class ParentStudentsController : BaseController
{
    private readonly IParentStudentService _parentStudentService;

    public ParentStudentsController(IParentStudentService parentStudentService)
    {
        _parentStudentService = parentStudentService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ParentStudentForCreationDto dto)
        => Ok(await _parentStudentService.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams @params)
        => Ok(await this._parentStudentService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._parentStudentService.RetrieveByIdAsync(id));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._parentStudentService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] ParentStudentForUpdateDto dto)
        => Ok(await this._parentStudentService.ModifyAsync(id, dto));

}
