﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBlog.Domain.Abstract;
using SuperBlog.Domain.Entities;

namespace SuperBlog.Domain.Concrete
{
    public class BlogContentCreator : IBlogContentCreator
    {
        private readonly IUrlManager _urlManager;
        private readonly IModerationProcessor _moderationProcessor;

        public BlogContentCreator(IUrlManager urlManager, IModerationProcessor moderationProcessor)
        {
            if (urlManager == null || moderationProcessor == null)
                throw new ArgumentNullException();
            this._urlManager = urlManager;
            this._moderationProcessor = moderationProcessor;
        }
        public int CreatePost(Post post, IPostRepository postRepository)
        {
            post.State = (int)PostStates.OnModeration;
            postRepository.SavePost(post);
            //send to moderator (email or something else)
            string url = _urlManager.GetEditUrl(post);
            _moderationProcessor.ProcessModeration(post.ToString(), url);

            return (int) PostCreateReturns.Success;
        }

        public int CreateCategory(Category category, ICategoryRepository categoryRepository)
        {
            if (categoryRepository.Categories.Any(cat=>cat.Name == category.Name))
                return (int)CategoryCreateReturns.AlreadyExist;

            category.State = (int) CategoryStates.OnModeration;
            categoryRepository.SaveCategory(category);
            //send to moderator (email or something else)
            string url = _urlManager.GetEditUrl(category);
            _moderationProcessor.ProcessModeration(category.ToString(), url);

            return (int) CategoryCreateReturns.Success;
        }
    }
}
