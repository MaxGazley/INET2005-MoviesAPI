using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //Constructor
        public MoviesController()
        {
            //initalize dependencies or services here
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            //return a list of movies here
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Inception" },
                new Movie { Id = 2, Title = "The Dark Knight" },
                new Movie { Id = 3, Title = "Interstellar" }
            };
            return Ok(movies);
        }
    }
}
