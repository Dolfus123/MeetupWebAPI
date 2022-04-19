using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("user")]
    public class User
    {
        [Column("UserId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        [ForeignKey(nameof(Meetup))]
        public Meetup Meetup { get; set; }
    }
}
