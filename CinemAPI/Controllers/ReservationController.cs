using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Input.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projectionRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly IMovieRepository movieRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly IRoomRepository roomRepo;

        public ReservationController(INewReservation newReservation,
            IProjectionRepository projectionRepository,
            IReservationRepository reservationRepository,
            IMovieRepository movieRepository,
            ICinemaRepository cinemaRepository,
            IRoomRepository roomRepository)
            : base(projectionRepository, reservationRepository)
        {
            this.newReservation = newReservation;
            this.projectionRepo = projectionRepository;
            this.reservationRepo = reservationRepository;
            this.movieRepo = movieRepository;
            this.cinemaRepo = cinemaRepository;
            this.roomRepo = roomRepository;
        }

        [HttpPost]
        public IHttpActionResult Index(ReservationCreationModel reservation)
        {
            if (reservation == null || reservation.ProjectionId.Equals(0))
            {
                return BadRequest();
            }

            NewReservationSummary summary = newReservation.New(new Reservation(reservation.Row, reservation.Column, reservation.ProjectionId));

            if (summary.IsCreated)
            {
                IReservationCreation currentReservation = this.reservationRepo.Get(reservation.ProjectionId, reservation.Row, reservation.Column);
                IProjection currentProjection = this.projectionRepo.GetById(reservation.ProjectionId);
                IMovie currentMovie = this.movieRepo.GetById(currentProjection.MovieId);
                IRoom currentRoom = this.roomRepo.GetById(currentProjection.RoomId);
                ICinema currentCinema = this.cinemaRepo.GetById(currentRoom.CinemaId);

                object result = new
                {
                    UniqueKeyOfReservation = currentReservation.UniqueId,
                    ProjectionStartDate = currentProjection.StartDate,
                    MovieName = currentMovie.Name,
                    CinemaName = currentCinema.Name,
                    RoomNumber = currentRoom.Number,
                    Row = currentReservation.Row,
                    Column = currentReservation.Column,
                };

                this.projectionRepo.DecreaseSeatsByOne(reservation.ProjectionId);

                return Ok(result);
            }

            return BadRequest(summary.Message);
        }
    }
}
