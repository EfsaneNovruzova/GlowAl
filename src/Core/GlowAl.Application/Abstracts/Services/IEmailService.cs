using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlowAl.Application.Abstracts.Services;

public interface IEmailService
{
    Task SendEmailAsync(List<string> toEmails, string subject, string body);
}