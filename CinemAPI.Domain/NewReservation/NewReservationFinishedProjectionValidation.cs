using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationFinishedProjectionValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projectionRepo;
        private readonly IMovieRepository movieRepo;

        public NewReservationFinishedProjectionValidation(INewReservation newReservation,
                                            IProjectionRepository projectionRepository,
                                            IMovieRepository movieRepository)
        {
            this.newReservation = newReservation;
            this.projectionRepo = projectionRepository;
            this.movieRepo = movieRepository;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            IProjection projection = this.projectionRepo.GetById(reservation.ProjectionId);
            IMovie curMovie = this.movieRepo.GetById(projection.MovieId);
            DateTime currentTime = DateTime.Now;

           bool isFinised = projection.StartDate.AddMinutes(curMovie.DurationMinutes) <= currentTime;

            if (isFinised)
            {
                return new NewReservationSummary(false, "This projection is already finished");
            }

            return newReservation.New(reservation);
        }
    }
}
