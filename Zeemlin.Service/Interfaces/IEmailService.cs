using Zeemlin.Domain.Entities;

namespace Zeemlin.Service.Interfaces;

public interface IEmailService
{
    public Task SendMessage(Message message);
    public Task<bool> SendCodeByEmailAsync(string email);

    public Task<bool> GetCodeAsync(string email, string code);
    Task SendEmailWithAttachment(string recipientEmail, string subject, string messageBody, byte[] attachmentData);
}
