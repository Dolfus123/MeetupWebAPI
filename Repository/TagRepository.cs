﻿using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using MeetupWebAPI.Entities.Models;

namespace MeetupWebAPI.Repository
{
    internal class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}