using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface INoteRepository
    {
        Guid Add(Note note);
        bool Remove(Note note);
        bool Remove(Guid noteById);
        bool __RemoveHard(Note note);
        bool __RemoveHard(Guid noteById);
        bool Update(Note note);
        Note? GetNoteById(Guid noteId);
        IQueryable<Note> GetNoteByIdAsQuerable(Guid noteId);
        IQueryable<Note>? GetAll();
        IQueryable<Note> _GetEveryOne();
        IQueryable<Note>? GetNoteByCompany();
        IQueryable<Note>? GetNoteByCompany(Guid companyId);
    }
}
