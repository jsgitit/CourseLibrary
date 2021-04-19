using AutoMapper;
using CourseLibrary.API.Helper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")] // using explicit routing
    // [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(
            ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet()]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors()
        {
            throw new Exception("Test Exception");
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            //var authors = new List<AuthorDTO>();
            //foreach (var author in authorsFromRepo)
            //{
            //    authors.Add(
            //        new AuthorDTO()
            //        {
            //            Id = author.Id,
            //            Name = $"{author.FirstName} {author.LastName}",
            //            MainCategory = author.MainCategory,
            //            Age = author.DateOfBirth.GetCurrentAge()
            //        });
            //}

            // automapper implementation replaces foreach
            return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(authorsFromRepo));

        }

        [HttpGet("{authorId}")]
        public ActionResult<AuthorDTO> GetAuthor(Guid authorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<AuthorDTO>(authorFromRepo));
        }
    }
}
