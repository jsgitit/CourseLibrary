using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseForCreationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        //public Guid AuthorId { get; set; }  // removed, since authorId will be in the route template already
    }
}
