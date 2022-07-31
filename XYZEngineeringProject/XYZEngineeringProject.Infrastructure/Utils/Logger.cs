
namespace XYZEngineeringProject.Infrastructure.Utils
{
    public class Logger
    {
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Context _context;

        public Logger(InfrastructureUtils infrastructureUtils, Context context)
        {
            _infrastructureUtils = infrastructureUtils;
            _context = context;
        }

        public void Log(Source source, InfoType infoType, string info)
        {
            _context.Logs.Add(new Domain.Models.EntityUtils.Log 
            {
                Source = source.ToString(),
                InfoType = infoType.ToString(),
                Info = info,
                UserId = _infrastructureUtils.GetUserIdFormHttpContext(),
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
