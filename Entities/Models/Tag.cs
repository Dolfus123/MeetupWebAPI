using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetupWebAPI.Entities.Models
{
    public class Tag
    {
        [Column("TagId")]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; }
        public Meetup Meetup { get; set; }
    }
}
