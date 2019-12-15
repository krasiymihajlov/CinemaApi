using System.Collections.Generic;
using System.Linq;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;

        public ReservationRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IReservationCreation Get(int projId, int row, int col)
        {
            return db.Reservations.Where(x => x.ProjectionId == projId &&
                                              x.Row == row &&
                                              x.Column == col).SingleOrDefault();
        }

        public IReservationCreation GetByUniqueId(string uniqueId)
        {
            return db.Reservations.Where(x => x.UniqueId == uniqueId).SingleOrDefault();
        }

        public void Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.Row, reservation.Column, reservation.ProjectionId);

            db.Reservations.Add(newReservation);
            db.SaveChanges();
        }
        
        public int CancelAllReservationForProjection(int projId)
        {
            List<Reservation> allReservationsForProjection = db.Reservations.Where(x => x.ProjectionId == projId).ToList();
            int canceledReservationCount = 0;
            if (allReservationsForProjection != null && allReservationsForProjection.Count > 0 && allReservationsForProjection.Any(x => !x.IsCanceled))
            {
                for (int i = 0; i < allReservationsForProjection.Count; i++)
                {
                    allReservationsForProjection[i].IsCanceled = true;
                    canceledReservationCount++;
                }

                db.SaveChanges();
            }

            return canceledReservationCount;
        }

        public bool UseReservation(int projId, int row, int col)
        {
            Reservation reservation = db.Reservations.Where(x => x.ProjectionId == projId &&
                                              x.Row == row &&
                                              x.Column == col).SingleOrDefault();

            if (reservation != null)
            {
                reservation.IsReservationUsed = true;
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
