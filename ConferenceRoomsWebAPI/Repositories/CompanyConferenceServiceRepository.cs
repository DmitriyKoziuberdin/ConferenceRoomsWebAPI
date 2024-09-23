﻿using ConferenceRoomsWebAPI.ApplicationDb;
using ConferenceRoomsWebAPI.Interfaces;

namespace ConferenceRoomsWebAPI.Repositories
{
    public class CompanyConferenceServiceRepository : ICompanyConferenceServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyConferenceServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
