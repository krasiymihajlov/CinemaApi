using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        IReservationCreation Get(int projId, int row, int col);

        void Insert(IReservationCreation reservation);

        IReservationCreation GetByUniqueId(string uniqueId);
        
        int CancelAllReservationForProjection(int projId);

        bool UseReservation(int projId, int row, int col);
    }
}
