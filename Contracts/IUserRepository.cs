using Entities.Models;
using System;
using System.Collections.Generic;

namespace MeetupWebAPI.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> UsersByMeetup(Guid ownerId);
    }
}

