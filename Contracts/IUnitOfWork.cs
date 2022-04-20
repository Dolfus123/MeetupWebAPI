using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Contracts
{
    public interface IUnitOfWork
    {
        IMeetupRepository Meetup { get; }
        ITagRepository Tag { get; }
        IUserRepository User { get; }
        Task<bool> CompleteAsync();
    }
}
