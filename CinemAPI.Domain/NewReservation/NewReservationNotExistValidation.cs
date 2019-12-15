using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationNotExistValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projRepo;

        public NewReservationNotExistValidation(INewReservation newReservation, IProjectionRepository projectionRepository)
        {
            this.newReservation = newReservation;
            this.projRepo = projectionRepository;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            IProjection projection = this.projRepo.GetById(reservation.ProjectionId);

            bool isSeatExist = reservation.Row * reservation.Column > projection.AvailableSeatsCount;

            if (isSeatExist)
            {
                return new NewReservationSummary(false, $"This seat not existing in a room");
            }

            return newReservation.New(reservation);
        }
    }
}
