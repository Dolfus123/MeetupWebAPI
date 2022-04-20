using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetupWebAPI.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<User> UsersByMeetup(Guid meetupId)
        {
            return FindByConditionAsync(a => a.MeetupId.Equals(meetupId)).ToList();
        }
    }
}
