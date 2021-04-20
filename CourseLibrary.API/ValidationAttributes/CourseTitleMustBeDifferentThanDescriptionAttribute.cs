using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttributes 
{
    public class CourseTitleMustBeDifferentThanDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForCreationDTO)validationContext.ObjectInstance;
            if (course.Title == course.Description) // sample rule
            {
                return new ValidationResult(
                    "The provided description should be different than the title.",
                    new[] { nameof(CourseForCreationDTO) });
            }
            return ValidationResult.Success;
        }
    }
}
