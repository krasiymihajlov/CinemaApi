using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);

        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        IList<Projection> GetActiveProjections();

        IProjection GetById(int id);

        void DecreaseSeatsByOne(int projId);

        void AddSeats(int projId, int seats);
    }
}