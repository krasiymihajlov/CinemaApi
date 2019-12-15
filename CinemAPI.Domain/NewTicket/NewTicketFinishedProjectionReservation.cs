using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketFinishedProjectionReservation : INewTicket
    {
        private readonly INewTicket newTicket;
        private readonly IProjectionRepository projectionRepo;
        private readonly IMovieRepository movieRepo;

        public NewTicketFinishedProjectionReservation(INewTicket newTicket,
                                            IProjectionRepository projectionRepository,
                                            IMovieRepository movieRepository)
        {
            this.newTicket = newTicket;
            this.projectionRepo = projectionRepository;
            this.movieRepo = movieRepository;
        }

        public NewTicketSummary New(ITicketCreation ticket)
        {
            IProjection projection = this.projectionRepo.GetById(ticket.ProjectionId);
            IMovie curMovie = this.movieRepo.GetById(projection.MovieId);
            DateTime currentTime = DateTime.Now;

            bool isFinished = projection.StartDate.AddMinutes(curMovie.DurationMinutes) <= currentTime;
            if (isFinished)
            {
                return new NewTicketSummary(false, "This projection is already finished");
            }

            return newTicket.New(ticket);
        }
    }
}
