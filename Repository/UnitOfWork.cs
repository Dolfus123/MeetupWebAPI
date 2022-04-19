using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RepositoryContext _repoContext;
        private IMeetupRepository _meetup;
        private IUserRepository _user;
        public IMeetupRepository Meetup
        {
            get
            {
                if (_meetup == null)
                {
                    _meetup = new MeetupRepository(_repoContext);
                }
                return _meetup;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
