﻿using HotelBooking.DataAccessLayer.Entities;

namespace HotelBooking.DataAccessLayer.Repositories.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<Room> GetRoom(RoomType roomType, int capacity, DateOnly startDate, DateOnly endDate);
}

