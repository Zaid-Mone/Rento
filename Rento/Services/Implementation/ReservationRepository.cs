using Microsoft.EntityFrameworkCore;
using Rento.Data;
using Rento.Models;
using Rento.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Services.Implementation
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Delete(Reservation reservation)
        {
            _context.Remove(reservation);
            Save();
        }

        public List<Reservation> GetAll()
        {
            return _context.Reservations.Include(i => i.Car).ThenInclude(u => u.CarType).ToList();
        }

        public Reservation GetById(Guid id)
        {
            return _context.Reservations.Include(i => i.Car).ThenInclude(u => u.CarType).FirstOrDefault(f => f.Id == id);
        }

        public void Insert(Reservation reservation)
        {
            _context.Add(reservation);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Reservation reservation)
        {
            _context.Update(reservation);
            Save();
        }
    }
}
