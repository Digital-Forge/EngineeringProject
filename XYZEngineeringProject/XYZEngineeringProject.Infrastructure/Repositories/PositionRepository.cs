using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;

        public PositionRepository(Context context, InfrastructureUtils infrastructureUtils, Logger logger)
        {
            _context = context;
            _infrastructureUtils = infrastructureUtils;
            _logger = logger;
        }

        public int Add(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add position - {position.Id}");
            return position.Id;
        }

        public IQueryable<Position> GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.Company == null)
                return _context.Positions
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);
            else
                return _context.Positions
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<Position>? GetPositionByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.Positions
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<Position>? GetPositionByCompany(Guid companyId)
        {
            return _context.Positions
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public Position? GetPositionById(int positionId)
        {
            return _context.Positions
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == positionId);
        }

        public IQueryable<Position> GetPositionByIdAsQuerable(int positionId)
        {
            return _context.Positions
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == positionId);
        }

        public bool Remove(Position position)
        {
            if (position == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null positions");
                return false;
            }

            try
            {
                _context.Positions.Remove(position);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove positions - {position.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove positions - {position.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(int positionById)
        {
            if (positionById == 0)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove position by 0 id");
                return false;
            }

            try
            {
                var buff = GetPositionById(positionById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove position, who don't exist or deleted");
                    return false;
                }

                _context.Positions.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove position - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove position - {positionById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(Position position)
        {
            if (position == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null position");
                return false;
            }

            try
            {
                _context.Positions.Update(position);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update position - {position.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update position - {position.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Position position)
        {
            try
            {
                _context.Positions.Remove(position);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove position - {position.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove position - {position.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(int positionById)
        {
            try
            {
                var buff = _context.Positions.FirstOrDefault(x => x.Id == positionById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove position, who don't exist");
                    return false;
                }

                _context.Positions.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove position - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove position - {positionById} - [{e.Message}]");
                return false;
            }
            return true;
        }
    }
}
