using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IProjection Get(int movieId, int roomId, DateTime startDate)
        {
            return db.Projections.FirstOrDefault(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }

        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public IList<Projection> GetActiveProjections()
        {
            DateTime now = DateTime.UtcNow;
            return db.Projections.Where(x => x.StartDate > now).ToList();
        }

        public void Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            db.Projections.Add(newProj);
            db.SaveChanges();
        }

        public IProjection GetById(int id)
        {
            return db.Projections.SingleOrDefault(x => x.Id == id);
        }

        public void DecreaseSeatsByOne(int projId)
        {
            Projection proj = db.Projections.SingleOrDefault(x => x.Id == projId);
            if (proj != null)
            {
                proj.AvailableSeatsCount -= 1;
                db.SaveChanges();
            }
        }

        public void AddSeats(int projId, int seats)
        {
            Projection proj = db.Projections.SingleOrDefault(x => x.Id == projId);
            if (proj != null)
            {
                proj.AvailableSeatsCount += seats;
                db.SaveChanges();
            }
        }
    }
}