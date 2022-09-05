
using Microsoft.AspNetCore.Http;

namespace XYZEngineeringProject.Infrastructure.Utils
{
    public class Logger
    {
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Context _context;

        public Logger(IHttpContextAccessor httpContextAccessor, Context context)
        {
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _context = context;
        }

        public void Log(Source source, InfoType infoType, string info)
        {
            _context.Logs.Add(new Domain.Models.EntityUtils.Log 
            {
                Source = source.ToString(),
                InfoType = infoType.ToString(),
                Info = info,
                UserId = _infrastructureUtils.GetUserIdFormHttpContext().ToString(),
                DataStamp = DateTime.Now
            });

            _context.SaveChanges();
        }

        public enum Source
        {
            Other,
            Controller,
            Service,
            Repository
        }

        public enum InfoType 
        {
            Info,
            Error,
            Warning
        }
    }
}
