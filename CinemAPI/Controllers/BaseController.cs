using CinemAPI.Data;
using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IReservationRepository reservationRepo;

        public BaseController(IProjectionRepository projectionRepo, 
            IReservationRepository reservationRepo)
        {
            this.projectionRepo = projectionRepo;
            this.reservationRepo = reservationRepo;
        }

        protected void CheckProjctionReservation()
        {
            IList<Projection> ativeProjections = this.projectionRepo.GetActiveProjections();
            DateTime currentTime = DateTime.Now;
            int addMinutes = -10;

            for (int i = 0; i < ativeProjections.Count(); i++)
            {
                Projection projection = ativeProjections[i];
                bool isStartingInLessThan10Min = projection.StartDate.AddMinutes(addMinutes) <= currentTime;
                bool isStarted = projection.StartDate < currentTime;

                if (isStartingInLessThan10Min && !isStarted)
                {
                    int canceledReservationCount = this.reservationRepo.CancelAllReservationForProjection(projection.Id);
                    if (canceledReservationCount > 0)
                    {
                        this.projectionRepo.AddSeats(projection.Id, canceledReservationCount);                        
                    }
                }
            }
        }
    }
}