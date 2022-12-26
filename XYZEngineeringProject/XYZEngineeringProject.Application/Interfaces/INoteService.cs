using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface INoteService
    {
        public List<NoteVM> GetAllNotes();
        public bool AddNote(NoteVM noteVM);
        public bool EditNote(NoteVM noteVM);
        bool DeleteNote(NoteVM noteVM);
    }
}
