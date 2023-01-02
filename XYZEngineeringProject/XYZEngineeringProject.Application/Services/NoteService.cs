using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        public bool AddNote(NoteVM noteVM)
        {
            Note note = new Note()
            {
                Title = noteVM.Title,
                NoteStatus = noteVM.NoteStatus,
                Date = noteVM.Date
            };

            _noteRepository.Add(note);

            return true;

        }

        public bool EditNote(NoteVM noteVM)
        {
            var note = _noteRepository.GetNoteById(noteVM.Id);
            if (note != null)
            {
                note.Date = noteVM.Date;
                note.Title = noteVM.Title;
                note.NoteStatus = noteVM.NoteStatus;
                return _noteRepository.Update(note);
            }
            return false;
        }

        public List<NoteVM> GetAllNotes()
        {
            return _noteRepository.GetAll().Select(x => new NoteVM
            {
                Date = x.Date,
                Title = x.Title,
                Id = x.Id,
                NoteStatus = x.NoteStatus,
                CreatedBy = x.CreateBy.ToString()
            }).ToList();
        }

        public bool DeleteNote(NoteVM noteVM)
        {
            return _noteRepository.Remove(noteVM.Id);
        }
    }
}
