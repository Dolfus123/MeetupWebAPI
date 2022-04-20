
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using System.Threading.Tasks;

namespace MeetupWebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RepositoryContext _repoContext;
        public IMeetupRepository Meetup => new MeetupRepository(_repoContext);
        public ITagRepository Tag => new TagRepository(_repoContext);
        public IUserRepository User => new UserRepository(_repoContext);
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task<bool> CompleteAsync() => await _repoContext.SaveChangesAsync() > 0;
    }
}
