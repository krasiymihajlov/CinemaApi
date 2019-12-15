using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionAvailableSeatsCountValidation : INewProjection
    {
        private readonly IProjectionRepository projectionsRepo;
        private readonly INewProjection newProj;


        public NewProjectionAvailableSeatsCountValidation(IProjectionRepository projectionsRepo, INewProjection newProj)
        {
            this.projectionsRepo = projectionsRepo;
            this.newProj = newProj;
        }

        public NewProjectionSummary New(IProjectionCreation projection)
        {
            if (projection.AvailableSeatsCount < 0)
            {
                return new NewProjectionSummary(false, $"The column AvailableSeatsCount can not accept negative values");
            }

            return newProj.New(projection);
        }
    }
}
