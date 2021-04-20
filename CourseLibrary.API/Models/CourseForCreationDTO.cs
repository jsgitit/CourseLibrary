using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseForCreationDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        
        //public Guid AuthorId { get; set; }  // removed, since authorId will be in the route template already
    }
}
