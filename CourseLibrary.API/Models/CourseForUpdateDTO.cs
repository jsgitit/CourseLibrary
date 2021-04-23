using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class CourseForUpdateDTO : CourseForManipulationDTO
    {
        // here we are overriding the Description so that we can add our extra Required attribute.
        [Required(ErrorMessage = "You should fill out a Description.")]
        public override string Description { get => base.Description; set =>base.Description = value; }

    }
}
