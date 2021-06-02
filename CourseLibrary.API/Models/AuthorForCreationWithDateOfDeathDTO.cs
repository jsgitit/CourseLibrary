using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class AuthorForCreationWithDateOfDeathDTO : AuthorForCreationDTO
    {
        public DateTimeOffset? DateOfDeath { get; set; }

    }
}
