using CompanyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPortal.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}
