using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Input.Projection;
using System;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : BaseController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionRepository projectionRepository;
        private readonly IMovieRepository movieRepository;

        public ProjectionController(INewProjection newProj, 
            IProjectionRepository projectionRepository,
            IMovieRepository movieRepository, 
            IReservationRepository reservationRepository)
            : base(projectionRepository, reservationRepository)
        {
            this.newProj = newProj;
            this.projectionRepository = projectionRepository;
            this.movieRepository = movieRepository;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate, model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAvailableSeats(int id)
        {
            IProjection projection = this.projectionRepository.GetById(id);

            if (projection == null)
            {
                return this.BadRequest("Projection not exist");
            }

            DateTime currentTime = DateTime.Now;
            IMovie curMovie = this.movieRepository.GetById(projection.MovieId);

            bool isFinished = projection.StartDate.AddMinutes(curMovie.DurationMinutes) <= currentTime;
            if (isFinished)
            {
                return this.BadRequest("This projection is already finished");
            }

            bool isStarting = projection.StartDate < currentTime;
            if (isStarting)
            {
                return this.BadRequest("Projection is already started");
            }

            return Ok(projection.AvailableSeatsCount);
        }
    }
}