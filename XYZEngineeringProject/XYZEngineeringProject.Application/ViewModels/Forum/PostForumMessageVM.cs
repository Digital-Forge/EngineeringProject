﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZEngineeringProject.Application.ViewModels.Forum
{
    public  class PostForumMessageVM
    {
        public Guid ForumId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}
