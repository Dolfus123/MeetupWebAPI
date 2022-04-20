using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetupWebAPI.Contracts
{
    public interface IMeetupRepository : IRepositoryBase<Meetup>
    {
        Task<IEnumerable<Meetup>> GetAllMeetupsAsync();
        Task<Meetup> GetMeetupByIdAsync(Guid meetupId);
        Task<Meetup> GetMeetupWithDetailsAsync(Guid meetupId);
        void CreateMeetup(Meetup meetup);
        void UpdateMeetup(Meetup meetup);
        void DeleteMeetup(Meetup meetup);
    }
}
