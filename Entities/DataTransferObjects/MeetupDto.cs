using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Entities.DataTransferObjects
{
    public class MeetupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfMeet { get; set; }
        public string Description { get; set; }
    }
}
