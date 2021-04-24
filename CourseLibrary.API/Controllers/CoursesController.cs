using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CoursesController(
            ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper
            )
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthorFromRepo = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDTO>>(coursesForAuthorFromRepo));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDTO> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDTO>(courseForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDTO> CreateCourseForAuthor(
            Guid authorId, CourseForCreationDTO course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseEntity = _mapper.Map<Entities.Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDTO>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor",
                new
                {
                    authorId = authorId,
                    courseId = courseToReturn.Id
                },
                courseToReturn);
        }

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourseForAuthor(
            Guid authorId,
            Guid courseId,
            CourseForUpdateDTO course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRepo == null)
            {
                // Implement Upsert logic, and insert the course
                // Client is generating the GUID for courseId in this case.
                var courseToAdd = _mapper.Map<Entities.Course>(course);
                courseToAdd.Id = courseId;
                _courseLibraryRepository.AddCourse(authorId, courseToAdd);
                _courseLibraryRepository.Save();
                var courseToReturn = _mapper.Map<CourseDTO>(courseToAdd);
                return CreatedAtRoute("GetCourseForAuthor",
                    new
                    {
                        authorId,
                        courseId = courseToReturn.Id
                    },
                    courseToReturn);

            }

            // Map the entity to a courseForUpdateDTO
            // Apply the update field values to that DTO
            // Map the courseForUpdateDTO back to an entity
            _mapper.Map(course, courseForAuthorFromRepo);
            // After the Map() above, the Entity will now contain the updated fields,
            // adhering to any projections we defined in the profile

            // Update the repo
            _courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);
            _courseLibraryRepository.Save();
            return NoContent();  // here we're not return the resource.
                                 // Some APIs might need the resource.  
                                 // In our implementation, it's up to the client
                                 // to decide on GET to update the resource.
                                 // Notice too, how the ActionResult does not contain a <T> to return.
        }

        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdateCourseForAuthor(
            Guid authorId,
            Guid courseId,
            JsonPatchDocument<CourseForUpdateDTO> patchDocument)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            
            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorRepo == null)
            {
                return NotFound();
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDTO>(courseForAuthorRepo);

            // add validation before using ApplyTo()
            patchDocument.ApplyTo(courseToPatch, ModelState);
            
            if(!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            // map the dto back to the entity
            _mapper.Map(courseToPatch, courseForAuthorRepo);

            _courseLibraryRepository.UpdateCourse(courseForAuthorRepo);

            _courseLibraryRepository.Save();
            
            return NoContent();
        }
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

    }
}
