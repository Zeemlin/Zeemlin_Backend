using Microsoft.AspNetCore.Mvc;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.Interfaces;

namespace Zeemlin.Api.Controllers;

public class EmailsController : BaseController
{
    private readonly IEmailService _emailService;
    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageAsync(Message message)
    {
        await _emailService.SendMessage(message);
        return Ok();
    }

    [HttpPost("send-code")]

    public async Task<IActionResult> VerifyEmailAsync(string email)
    {
        var data = await _emailService.SendCodeByEmailAsync(email);

        return Ok(data);
    }


    [HttpPost("verify-code")]

    public async Task<IActionResult> GetCodeAsync(string email, string code)
    {
        var data = await _emailService.GetCodeAsync(email, code);
        return Ok(data);
    }
}
