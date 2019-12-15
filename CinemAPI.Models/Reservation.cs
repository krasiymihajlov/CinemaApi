using CinemAPI.Models.Contracts.Reservation;
using System;

namespace CinemAPI.Models
{
    public class Reservation : IReservationCreation
    {
        public Reservation()
        {
        }

        public Reservation(int row, int colum, int projectionId) 
            : this()
        {
            this.Row = row;
            this.Column = colum;
            this.ProjectionId = projectionId;
            this.UniqueId = Guid.NewGuid().ToString();
            this.IsCanceled = false;
            this.IsReservationUsed = false;
        }

        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsReservationUsed { get; set; }

        public string UniqueId { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }
    }
}
