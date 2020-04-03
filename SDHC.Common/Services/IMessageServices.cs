using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDHC.Common.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public interface IAuthMessageSender : IEmailService, ISmsService
    {
        
    }
}
