using Entities.Models;
using MeetupWebAPI.Contracts;
using MeetupWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Meetup>> GetAllMeetupsAsync()
        {
            return await FindAllAsync()
                    .OrderBy(meetup => meetup.Title)
                    .ToListAsync();
        }
        public async Task<Meetup> GetMeetupByIdAsync(Guid meetupId)
        {
            return await FindByConditionAsync(meetup => meetup.Id.Equals(meetupId))
                .FirstOrDefaultAsync();
        }
        public async Task<Meetup> GetMeetupWithDetailsAsync(Guid meetupId)
        {
            return await FindByConditionAsync(meetup => meetup.Id.Equals(meetupId))
                .Include(user => user.Users)
                .FirstOrDefaultAsync();
        }
        public void CreateMeetup(Meetup meetup) => Create(meetup);
        public void UpdateMeetup(Meetup meetup) => Update(meetup);
        public void DeleteMeetup(Meetup meetup) => Delete(meetup);
    }
}
