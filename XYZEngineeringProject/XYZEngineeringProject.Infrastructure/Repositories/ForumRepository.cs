using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models.Forum;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Infrastructure.Repositories
{
    public class ForumRepository : IForumRepository
    {
        private readonly Context _context;
        private readonly InfrastructureUtils _utils;

        public ForumRepository(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _utils = new InfrastructureUtils(context, httpContextAccessor);
        }

        public Guid? AddForum(Forum forum)
        {
            _context.Forums.Add(forum);
            _context.SaveChanges();
            return forum.Id;
        }

        public Guid? AddMessage(ForumMessage post)
        {
            if (post == null) return null;
            if (post.Content == null)
                post.Content = "";

            _context.ForumMessages.Add(post);
            _context.SaveChanges();
            return post.Id;
        }

        public void DeleteForum(Guid id)
        {
            var forum = _context.Forums.Include(i => i.ForumMessages).FirstOrDefault(x => x.Id == id);

            if (forum?.ForumMessages != null)
                foreach (var item in forum.ForumMessages)
                {
                    item.UseStatus = Domain.Models.EntityUtils.UseStatusEntity.Delete;
                    _context.Remove(item);
                }

            forum.UseStatus = Domain.Models.EntityUtils.UseStatusEntity.Delete;
            _context.Remove(forum);
            _context.SaveChanges();
        }

        public void DeleteForumMessage(Guid id)
        {
            var message = _context.ForumMessages.FirstOrDefault(x => x.Id == id);
            if (message != null)
            {
                message.UseStatus = Domain.Models.EntityUtils.UseStatusEntity.Delete;
                _context.Remove(message);
                _context.SaveChanges();
            }
        }
    }
}
