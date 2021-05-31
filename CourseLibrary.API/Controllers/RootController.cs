using CourseLibrary.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
            
        {
            // Create links for root http://../api 

            var links = new List<LinkDTO>();
            links.Add(new LinkDTO(Url.Link("GetRoot", new { }), "self", "GET"));
            links.Add(new LinkDTO(Url.Link("GetAuthors", new { }), "authors", "GET"));
            links.Add(new LinkDTO(Url.Link("CreateAuthor", new { }), "create_author", "POST"));
            return Ok(links);
        }
    }
}
