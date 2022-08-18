using Rento.Models;
using System;
using System.Collections.Generic;

namespace Rento.Services.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetAll();
        Reservation GetById(Guid id);
        void Insert(Reservation reservation);
        void Delete(Reservation reservation);
        void Update(Reservation reservation);
        void Save();
    }
}
