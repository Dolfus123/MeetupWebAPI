using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Repository
{
    public class MeetupRepository : RepositoryBase<Meetup>, IMeetupRepository
    {
        public MeetupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
