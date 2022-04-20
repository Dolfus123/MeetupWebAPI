using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Entities.DataTransferObjects
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
