using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.Input.Reservation;
using CinemAPI.Models.Input.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : BaseController
    {
        private readonly INewTicket newTicket;
        private readonly INewTicketWithReservation newTicketWithReservation;
        private readonly IProjectionRepository projectionRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly IMovieRepository movieRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly IRoomRepository roomRepo;

        public TicketController(INewTicket newTicket,
            INewTicketWithReservation newTicketWithReservation,
            IProjectionRepository projectionRepository,
            ITicketRepository ticketRepository,
            IMovieRepository movieRepository,
            ICinemaRepository cinemaRepository,
            IRoomRepository roomRepository,
            IReservationRepository reservationRepository)
            : base(projectionRepository, reservationRepository)
        {
            this.newTicket = newTicket;
            this.newTicketWithReservation = newTicketWithReservation;
            this.projectionRepo = projectionRepository;
            this.ticketRepo = ticketRepository;
            this.movieRepo = movieRepository;
            this.cinemaRepo = cinemaRepository;
            this.roomRepo = roomRepository;
            this.reservationRepo = reservationRepository;
        }

        [HttpPost]
        public IHttpActionResult Index(TicketCreationModel ticket)
        {
            return BadRequest();
        }

        [HttpPost]

        public IHttpActionResult WithReservation(ReservationCreationModel reservation)
        {
            IReservationCreation currentReservation = this.reservationRepo.GetByUniqueId(reservation.UniqueId);
            if (currentReservation == null)
            {
                return this.BadRequest();
            }

            NewTicketSummary ticket = newTicketWithReservation.New(new Ticket(currentReservation.ProjectionId, currentReservation.Row, currentReservation.Column));
           
            if (ticket.IsCreated) 
            {
                bool isReservationUsed = this.reservationRepo.UseReservation(currentReservation.ProjectionId, currentReservation.Row, currentReservation.Column);
                ITicketCreation currentTicket = this.ticketRepo.Get(currentReservation.ProjectionId, currentReservation.Row, currentReservation.Column);
                object result = ReturnTicketInformation(currentTicket);

                return Ok(result);
            }

            return BadRequest(ticket.Message);
        }

        [HttpPost]
        public IHttpActionResult WithoutReservation(TicketCreationModel currentTicket)
        {
            NewTicketSummary ticket = newTicket.New(new Ticket(currentTicket.ProjectionId, currentTicket.Row, currentTicket.Column));

            if (ticket.IsCreated)
            {
                ITicketCreation newTicket = this.ticketRepo.Get(currentTicket.ProjectionId, currentTicket.Row, currentTicket.Column);
                this.projectionRepo.DecreaseSeatsByOne(newTicket.ProjectionId);
                object result = ReturnTicketInformation(newTicket);               

                return Ok(result);
            }

            return BadRequest(ticket.Message);
        }

        private object ReturnTicketInformation(ITicketCreation currentTicket)
        {
            IProjection currentProjection = this.projectionRepo.GetById(currentTicket.ProjectionId);
            IMovie currentMovie = this.movieRepo.GetById(currentProjection.MovieId);
            IRoom currentRoom = this.roomRepo.GetById(currentProjection.RoomId);
            ICinema currentCinema = this.cinemaRepo.GetById(currentRoom.CinemaId);

            object result = new
            {
                TicketUniqueKey = currentTicket.UniqueId,
                ProjectionStartDate = currentProjection.StartDate,
                MovieName = currentMovie.Name,
                CinemaName = currentCinema.Name,
                RoomNumber = currentRoom.Number,
                Row = currentTicket.Row,
                Column = currentTicket.Column,
            };

            return result;
        }
    }
}