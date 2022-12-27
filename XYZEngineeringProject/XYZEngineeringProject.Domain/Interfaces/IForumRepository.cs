using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Models.Forum;

namespace XYZEngineeringProject.Domain.Interfaces
{
    public interface IForumRepository
    {
        Guid? AddMessage(ForumMessage post);
        Guid? AddForum(Forum forum);

        void DeleteForum(Guid id);
        void DeleteForumMessage(Guid id);
    }
}
