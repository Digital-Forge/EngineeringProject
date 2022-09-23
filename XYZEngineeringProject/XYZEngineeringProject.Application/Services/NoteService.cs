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
            throw new NotImplementedException();
        }

        public List<NoteVM> GetAllNotes()
        {
            throw new NotImplementedException();
        }
    }
}
