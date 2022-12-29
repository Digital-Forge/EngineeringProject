using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.Forum;
using XYZEngineeringProject.Domain.Interfaces;
using XYZEngineeringProject.Domain.Models.Forum;
using XYZEngineeringProject.Infrastructure.Utils;

namespace XYZEngineeringProject.Application.Services
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly InfrastructureUtils _utils;
        private readonly Context _context;

        public ForumService(IForumRepository forumRepository, Context context, IHttpContextAccessor httpContextAccessor)
        {
            _forumRepository = forumRepository;
            _context = context;
            _utils = new InfrastructureUtils(context, httpContextAccessor);
        }

        public Guid? AddForum(string name)
        {
            return _forumRepository.AddForum(new Forum
            {
                Name = name,
            });
        }

        public void DeleteForum(Guid id)
        {
            _forumRepository.DeleteForum(id);
        }

        public void DeleteForumMessage(Guid id)
        {
            _forumRepository.DeleteForumMessage(id);
        }

        public ForumVM GetForum(Guid id, int take = 20, int skip = 0)
        {
            var forum = _context.Forums
                .Where(x =>
                    x.Id == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .Select(s => new ForumVM
                {
                    Id = s.Id,
                    Name = s.Name
                }).FirstOrDefault();

            if (forum != null)
                forum.Content = GetForumContent(id, take, skip);
            return forum;
        }

        public List<ForumMessageVM> GetForumContent(Guid id, int take = 20, int skip = 0)
        {
            var query = _context.ForumMessages
                .Where(x =>
                    x.ForumId == id
                    && x.UseStatus != Domain.Models.EntityUtils.UseStatusEntity.Delete)
                .OrderByDescending(o => o.CreateDate)
                .Skip(skip)
                .Take(take)
                .Select(s => new ForumMessageVM
                {
                    Id = s.Id,
                    //Author = GetAuthor(s.CreateBy),
                    AuthorId = s.CreateBy,
                    Text = s.Content,
                    Date = s.CreateDate
                }).ToList(); ;

            foreach (var item in query)
            {
                item.Author = GetAuthor(item.AuthorId);
            }

            return query;
        }

        public string GetAuthor(Guid? id)
        {
            if (id == null) return string.Empty;
            
            var user = _context.AppUsers.FirstOrDefault(x => x.Id == id);
            if (user == null) return string.Empty;

            return user.Firstname + " " + user.Surname;
        }

        public void PostMessage(PostForumMessageVM post)
        {
            if (post == null) return;
            var user = _utils.GetUserFormHttpContext();
            if (user == null) return;

            _forumRepository.AddMessage(new ForumMessage
            {
                CreateBy = post.UserId,
                Content = post.Text,
                ForumId = post.ForumId,
            });
        }

        public Dictionary<Guid, string> GetUserForums(Guid userId)
        {
            var listForums = _context.AppUsers
                .Include(i => i.UsersToDepartments)
                    .ThenInclude(i => i.Departments)
                .Where(x => x.Id == userId)
                .SelectMany(x => x.UsersToDepartments)
                .Select(x => x.Departments)
                .Select(x => x.Forum)
                .ToList();

            var forums = _context.Forums
                .Where(x => listForums.Any(a => a.Value == x.Id))
                .ToDictionary(d => d.Id, d => d.Name);

            return forums;
        }
    }
}
