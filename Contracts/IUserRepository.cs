using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Contracts
{
    interface IUserRepository : IRepositoryBase<User>
    {
    }
}
