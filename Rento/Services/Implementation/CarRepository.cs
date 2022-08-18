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
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Delete(Car car)
        {
            _context.Cars.Remove(car);
            Save();
        }

        public List<Car> GetAll()
        {
            return _context.Cars.Include(b=>b.CarType).ToList();
        }

        public Car GetById(Guid id)
        {
            return _context.Cars.Include(b => b.CarType).FirstOrDefault(t=>t.Id==id);
        }

        public void Insert(Car car)
        {
            _context.Cars.Add(car);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            _context.Cars.Update(car);
            Save();
        }
    }
}
