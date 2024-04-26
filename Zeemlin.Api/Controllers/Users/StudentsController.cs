using Microsoft.AspNetCore.Mvc;
using Zeemlin.Service.DTOs.Users.Students;
using Zeemlin.Service.Interfaces.Users;

namespace Zeemlin.Api.Controllers.Users;

//[Authorize]
public class StudentsController : BaseController
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentForCreationDto dto)
        => Ok(await this._studentService.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this._studentService.RetrieveAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._studentService.RetrieveByIdAsync(id));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await this._studentService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] StudentForUpdateDto dto)
        => Ok(await this._studentService.ModifyAsync(id, dto));

    [HttpGet("search/{searchTerm}")]
    public async Task<IActionResult> SearchStudentsTerm(string searchTerm)
    {
        return Ok(await _studentService.RetrieveByDataAsync(searchTerm));
    }
}
