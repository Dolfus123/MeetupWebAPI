using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Tag")]
    public class Tag
    {
        [Column("TagId")]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; }
        public int MeetupId { get; set; }
        public Meetup Meetup { get; set; }
    }
}
