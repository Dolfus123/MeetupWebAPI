using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
