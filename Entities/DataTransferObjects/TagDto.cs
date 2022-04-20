using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupWebAPI.Entities.DataTransferObjects
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
