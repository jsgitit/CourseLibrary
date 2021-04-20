using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentThanDescription(ErrorMessage = "Title must be different than Description.")]
    public class CourseForCreationDTO //: IValidatableObject
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "The title should not have more than 100 characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "The description should not have more than 1500 characters" )]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description) // sample rule
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different than the title.",
        //            new[] { "CourseForCreationDTO" });

        //            // SAMPLE ERROR in response body:
        //            //            {
        //            //                "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        //            //                "title": "One or more validation errors occurred.",
        //            //                "status": 400,
        //            //                "traceId": "|98fb9c26-47761db3b03e5dec.",
        //            //                "errors": {
        //            //                    "CourseForCreationDTO": [
        //            //                        "The provided description should be different than the title."
        //            //    ]
        //            //}
        //            //            }

        //    }
        //}

        //public Guid AuthorId { get; set; }  // removed, since authorId will be in the route template already
    }
}
