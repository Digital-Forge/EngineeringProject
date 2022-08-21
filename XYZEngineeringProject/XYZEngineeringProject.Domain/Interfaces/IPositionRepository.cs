using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IPositionRepository
    {
        Guid Add(Position position);
        bool Remove(Position position);
        bool Remove(Guid positionById);
        bool __RemoveHard(Position position);
        bool __RemoveHard(Guid positionById);
        bool Update(Position position);
        Position? GetPositionById(Guid positionId);
        IQueryable<Position> GetPositionByIdAsQuerable(Guid positionId);
        IQueryable<Position>? GetAll();
        IQueryable<Position> _GetEveryOne();
        IQueryable<Position>? GetPositionByCompany();
        IQueryable<Position>? GetPositionByCompany(Guid companyId);
    }
}
