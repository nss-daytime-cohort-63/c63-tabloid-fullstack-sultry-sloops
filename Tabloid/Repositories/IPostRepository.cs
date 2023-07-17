﻿using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAll();
        List<Post> GetAllPastApprovedPosts();
        Post GetPublishedPostById(int id);
    }
}