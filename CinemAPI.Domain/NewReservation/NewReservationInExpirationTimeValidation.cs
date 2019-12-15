using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationInExpirationTimeValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projectionRepo;

        public NewReservationInExpirationTimeValidation(INewReservation newReservation, 
                                                        IProjectionRepository projectionRepository)
        {
            this.newReservation = newReservation;
            this.projectionRepo = projectionRepository;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            IProjection projection = this.projectionRepo.GetById(reservation.ProjectionId);
            DateTime currentTime = DateTime.Now;
            bool isStartingInLessThan10Min = projection.StartDate.AddMinutes(-10) <= currentTime;

            if (isStartingInLessThan10Min)
            {
                return new NewReservationSummary(false, $"You cannot reserve seats because projection starting in less than 10 minutes");
            }

            return newReservation.New(reservation);
        }
    }
}
