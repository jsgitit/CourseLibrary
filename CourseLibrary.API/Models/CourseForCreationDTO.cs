using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseForCreationDTO : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == Description) // sample rule
            {
                yield return new ValidationResult(
                    "The provided description should be different than the title.",
                    new[] { "CourseForCreationDTO" });
            }
        }

        //public Guid AuthorId { get; set; }  // removed, since authorId will be in the route template already
    }
}
