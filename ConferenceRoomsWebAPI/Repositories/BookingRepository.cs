﻿using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
