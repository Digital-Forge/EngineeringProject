using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.ViewModels.Forum;

namespace XYZEngineeringProject.Application.Interfaces
{
    public interface IForumService
    {
        List<ForumMessageVM> GetForumContent(Guid id, int take = 20, int skip = 0);
        void PostMessage(PostForumMessageVM post);
        ForumVM GetForum(Guid id, int take = 20, int skip = 0);

        Guid? AddForum(string name);

        void DeleteForum(Guid id);
        void DeleteForumMessage(Guid id);

        List<ForumVM> GetUserForums(Guid userId);

        List<ForumVM> GetAllCompanyForumsByCompany(Guid id);

    }
}
