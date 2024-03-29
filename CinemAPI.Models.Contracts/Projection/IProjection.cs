﻿using System;

namespace CinemAPI.Models.Contracts.Projection
{
    public interface IProjection
    {
        int Id { get; }

        int RoomId { get; }

        int MovieId { get; }

        DateTime StartDate { get; }

        int AvailableSeatsCount { get; }
    }
}