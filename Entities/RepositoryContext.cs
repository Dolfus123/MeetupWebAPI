using Entities.Models;
using MeetupWebAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetupWebAPI.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) {}
        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
