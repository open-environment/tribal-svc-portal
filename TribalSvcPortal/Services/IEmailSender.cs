using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TribalSvcPortal.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string To, string subject, string message, string from);
    }
}
