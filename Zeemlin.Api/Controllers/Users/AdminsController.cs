﻿using Microsoft.AspNetCore.Mvc;
using Zeemlin.Data.DbContexts;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.DTOs.Users.Admins;
using Zeemlin.Service.Exceptions;
using Zeemlin.Service.Interfaces.Users;

namespace Zeemlin.Api.Controllers.Users;

public class AdminsController : BaseController
{
    private readonly IAdminService adminService;
    private readonly AppDbContext appDbContext;

    public AdminsController(IAdminService adminService, AppDbContext appDbContext)
    {
        this.adminService = adminService;
        this.appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AdminForCreationDto dto)
        => Ok(await adminService.CreateAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(await adminService.RetrieveAllAsync(@params));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(await adminService.RetrieveByIdAsync(id));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(await adminService.RemoveAsync(id));

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] AdminForUpdateDto dto)
        => Ok(await adminService.ModifyAsync(id, dto));

    [HttpGet("username")]
    public async Task<IActionResult> SearchAdminsTerm(string admin)
    {
        return Ok(await adminService.SearchAdmins(admin));
    }

    [HttpGet("schools/{schoolId}/admins")]
    public async Task<IActionResult> RetrieveBySchoolIdAsync(long schoolId, [FromQuery] PaginationParams @params)
    {
        var admins = await adminService.RetrieveBySchoolIdAsync(schoolId, @params);
        return Ok(admins);
    }

}
