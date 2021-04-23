using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentThanDescription(ErrorMessage = "Title must be different than Description.")]
    public abstract class CourseForManipulationDTO
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "The title should not have more than 100 characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "The description should not have more than 1500 characters")]
        public virtual string Description { get; set; } // use of virtual allows us to
                                                        // override, since we have an implementation
                                                        // in the base class
    }
}
