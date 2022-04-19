using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Contracts
{
    interface IUnitOfWork
    {
        IMeetupRepository Meetup { get; }
        IUserRepository User { get; }
        void Save();//Task<bool> CompleteAsync();
    }
}
