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
        [Required(ErrorMessage = "You should fill out a Description.")]
        public override string Description { get => base.Description; set =>base.Description = value; }

    }
}
