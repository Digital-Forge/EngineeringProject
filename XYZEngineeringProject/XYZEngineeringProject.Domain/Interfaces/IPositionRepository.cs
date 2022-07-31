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
        int Add(Position position);
        bool Remove(Position position);
        bool Remove(int positionById);
        bool __RemoveHard(Position position);
        bool __RemoveHard(int positionById);
        bool Update(Position position);
        Position? GetPositionById(int positionId);
        IQueryable<Position> GetPositionByIdAsQuerable(int positionId);
        IQueryable<Position> GetAll();
        IQueryable<Position>? GetPositionByCompany();
        IQueryable<Position>? GetPositionByCompany(Guid companyId);
    }
}
