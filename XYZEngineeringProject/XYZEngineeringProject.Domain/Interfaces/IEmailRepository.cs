using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IEmailRepository
    {
        EmailConfig? GetCurrentEmailConfig();
        EmailConfig? GetEmailConfigByCompany(Guid companyId);
        EmailConfig? GetEmailConfigByUser(Guid userId);
        EmailConfig? GetEmailConfigByUser(AppUser user);
    }
}
