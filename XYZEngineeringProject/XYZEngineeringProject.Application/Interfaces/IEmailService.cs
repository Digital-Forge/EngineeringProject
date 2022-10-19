using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IEmailService
    {
        bool SendMail(string to, string title, string text);
    }
}
