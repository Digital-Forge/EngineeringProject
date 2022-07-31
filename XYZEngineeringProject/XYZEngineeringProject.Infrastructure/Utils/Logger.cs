
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

        public void Log(Source source, string info)
        {
            _context.Logs.Add(new Domain.Models.EntityUtils.Log 
            {
                Info = info,
                Source = source.ToString(),
                UserId = _infrastructureUtils.GetUserIdFormHttpContext()
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
    }
}
