using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;

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