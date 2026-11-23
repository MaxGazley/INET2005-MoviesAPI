using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using System.Data;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IDbConnection _db;

        //Constructor
        public MoviesController(IDbConnection db)
        {
            //initalize dependencies or services here
            _db = db;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
        {
            //return a list of movies here
            var sql = @"SELECT m.*, g.*, AR.*
                        FROM Movie m
                        INNER JOIN Genre g ON m.GenreId = g.Id
                        INNER JOIN AudienceRating AR on m.AudienceRatingId = AR.Id";

            // fetch the movies from the database <table 1, table 2, output type>
            var movies = await _db.QueryAsync<Movie, Genre, AudienceRating, Movie>(sql, (movie, genre, audienceRating) =>
            {
                movie.Genre = genre;
                movie.AudienceRating = audienceRating;
                return movie;
            });
            //, splitOn: "Id"); // indicates where to split the result set into multiple objects (first column of next table)

            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieByID(int id)
        {
            var sql = @"SELECT m.*, g.*, AR.*, CR.*, US.*
                        FROM Movie m
                        INNER JOIN Genre g ON m.GenreId = g.Id
                        INNER JOIN AudienceRating AR on m.AudienceRatingId = AR.Id
                        LEFT OUTER JOIN CriticReview CR on m.Id = CR.MovieId
                        LEFT OUTER JOIN UserSnapshot US on CR.CreatedBy = US.UserId
                        WHERE m.Id = @Id";

            Movie? movieResult = null;
            await _db.QueryAsync<Movie, Genre, AudienceRating, CriticReview, User, Movie>(sql, (movie, genre, audienceRating, criticReview, userSnapshot) =>
            {
                if (movieResult == null)
                {
                    movie.Genre = genre;
                    movie.AudienceRating = audienceRating;
                    movie.CriticReviews = new List<CriticReview>();
                    movieResult = movie;
                }

                if (criticReview != null)
                {
                    if (criticReview.IsPublished == true)
                    { 
                        if (userSnapshot != null)
                        {
                            criticReview.User = userSnapshot;
                        }
                        movieResult.CriticReviews.Add(criticReview);
                    }
                    
                }

                return movieResult;
            }, new { Id = id }, splitOn: "Id,Id,Id,UserId");

            if (movieResult == null)
            {
                return NotFound();
            }

            return Ok(movieResult);

        }




    }
}
