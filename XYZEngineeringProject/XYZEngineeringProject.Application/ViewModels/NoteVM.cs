using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models;

namespace XYZEngineeringProject.Application.ViewModels
{
    public class NoteVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? NoteStatus { get; set; }
        public bool isCompany { get; set; }
        public DateTime Date { get; set; }

        public string CreatedBy { get; set; }
    }
}
