using MimeKit;
using MailKit.Net.Smtp;
using Zeemlin.Domain.Entities;
using Zeemlin.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Zeemlin.Service.Exceptions;

namespace Zeemlin.Service.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;

    public EmailService(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _configuration = configuration.GetSection("Email");
        _memoryCache = memoryCache;
    }

    public async Task<bool> GetCodeAsync(string email, string code)
    {
        var cashedValue = _memoryCache.Get<string>(email);

        if (cashedValue?.ToString() != code)
        {
            throw new ZeemlinException(400, "Verification code not found or expired");
        }

        return true;
    }

    public async Task SendEmailWithAttachment(string recipientEmail, string subject, string messageBody, byte[] attachmentData)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(recipientEmail));

        email.Subject = subject;

        var multipart = new Multipart("mixed");
        email.Body = multipart;

        // Add the text part to the multipart message
        var textPart = new TextPart("html")
        {
            Text = messageBody
        };
        multipart.Add(textPart);

        // Add the PDF attachment to the multipart message
        var attachmentMessage = new MimeMessage();
        attachmentMessage.Body = new TextPart("plain") { Text = "This is the attachment content" }; // Or any other content type

        var attachmentPart = new MessagePart("message/rfc822")
        {
            Message = attachmentMessage
        };
        attachmentPart.ContentDisposition = new ContentDisposition("attachment") { FileName = "RegistrationConfirmation.pdf" };


        multipart.Add(attachmentPart);


        var smtp = new SmtpClient();

        await smtp.ConnectAsync(_configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }



    public async Task SendMessage(Message message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(message.To));

        email.Subject = message.Subject;
        email.Body = new TextPart("html")
        {
            Text = message.Body
        };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(_configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task<bool> SendCodeByEmailAsync(string email)
    {
        var randomNumber = new Random().Next(1000, 9999);

        var message = new Message()
        {
            To = email,
            Subject = "Zeemlin Verification",
            Body = $"Your code - {randomNumber}"
        };

        _memoryCache.Set(email, randomNumber.ToString(), TimeSpan.FromMinutes(2));
        await SendMessage(message);

        return true;
    }
}
