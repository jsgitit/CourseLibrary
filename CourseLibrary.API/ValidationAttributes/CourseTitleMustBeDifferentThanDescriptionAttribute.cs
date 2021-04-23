using CourseLibrary.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ValidationAttributes 
{/// <summary>
/// This class contains an example of how to use a custom validation attribute to use on Creation DTOs
/// However, a better approach to validation might be to use FluentValidation for complex rules, 
/// because they are easier to test and separate the rules from the mdoels.
/// </summary>
    public class CourseTitleMustBeDifferentThanDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var course = (CourseForManipulationDTO)validationContext.ObjectInstance;
            if (course.Title == course.Description) // sample rule
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(CourseForManipulationDTO) });
            }
            return ValidationResult.Success;
        }
    }
}
