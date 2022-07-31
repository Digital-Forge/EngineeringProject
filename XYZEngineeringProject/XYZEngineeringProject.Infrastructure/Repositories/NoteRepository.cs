using Microsoft.AspNetCore.Http;
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
    public class NoteRepository : INoteRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _infrastructureUtils;
        private readonly Logger _logger;

        public NoteRepository(Context context, IHttpContextAccessor httpContextAccessor, Logger logger)
        {
            _context = context;
            _infrastructureUtils = new InfrastructureUtils(context, httpContextAccessor);
            _logger = logger;
        }

        public Guid Add(Note note)
        {
            _context.Note.Add(note);
            _context.SaveChanges();

            _logger.Log(Logger.Source.Repository, Logger.InfoType.Info, $"Add note - {note.Id}");
            return note.Id;
        }

        public IQueryable<Note> GetAll()
        {
            var currentUser = _infrastructureUtils.GetUserFormHttpContext();

            if (currentUser?.Company == null)
                return _context.Note
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete);
            else
                return _context.Note
                    .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                    .Where(x => x.CompanyId == currentUser.CompanyId);
        }

        public IQueryable<Note>? GetNoteByCompany()
        {
            var companyId = _infrastructureUtils.GetCompany()?.Id;

            if (companyId == null) return null;

            return _context.Note
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.CompanyId == companyId);
        }

        public IQueryable<Note>? GetNoteByCompany(Guid companyId)
        {
            return _context.Note
                 .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                 .Where(x => x.CompanyId == companyId);
        }

        public Note? GetNoteById(Guid noteId)
        {
            return _context.Note
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .FirstOrDefault(x => x.Id == noteId);
        }

        public IQueryable<Note> GetNoteByIdAsQuerable(Guid noteId)
        {
            return _context.Note
                .Where(x => x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Where(x => x.Id == noteId);
        }

        public bool Remove(Note note)
        {
            if (note == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove null note");
                return false;
            }

            try
            {
                _context.Note.Remove(note);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove note - {note.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove note - {note.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Remove(Guid noteById)
        {
            if (noteById == Guid.Empty)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove note by empty guid");
                return false;
            }

            try
            {
                var buff = GetNoteById(noteById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying remove note, who don't exist or deleted");
                    return false;
                }

                _context.Note.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Remove note - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed remove note - {noteById} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool Update(Note note)
        {
            if (note == null)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying update null note");
                return false;
            }

            try
            {
                _context.Note.Update(note);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Update note - {note.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed update note - {note.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Note note)
        {
            try
            {
                _context.Note.Remove(note);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove note - {note.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove note - {note.Id} - [{e.Message}]");
                return false;
            }
            return true;
        }

        public bool __RemoveHard(Guid noteById)
        {
            try
            {
                var buff = _context.Note.FirstOrDefault(x => x.Id == noteById);
                if (buff == null)
                {
                    _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, "Trying hard remove note, who don't exist");
                    return false;
                }

                _context.Note.Remove(buff);
                _context.SaveChanges();

                _logger.Log(Logger.Source.Repository, Logger.InfoType.Warning, $"Hard remove note - {buff?.Id}");
            }
            catch (Exception e)
            {
                _logger.Log(Logger.Source.Repository, Logger.InfoType.Error, $"Failed hard remove note - {noteById} - [{e.Message}]");
                return false;
            }
            return true;
        }
    }
}
