using Microsoft.AspNetCore.Mvc;
using Rento.Data;
using Rento.Models;
using Rento.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Services.Implementation
{
    public class CarTypeRepository: ICarTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public CarTypeRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        //public CarType Check(CarType carType)
        //{
        //    var isExist = _context.CarTypes.Any(m => m.Name == carType.Name);
            
        //       if (isExist)
        //      {
                
        //        return ;
        //   }
        //    return carType;
        //}

        //public IActionResult Check(CarType carType)
        //{
        //    var isExist = _context.CarTypes.Any(m => m.Name == carType.Name);
        //    if (isExist)
        //    {
        //        return View(carType);
        //    }

        //}

        //public void CheckExist(CarType carType)
        //{
        //    var isExist = _context.CarTypes.Any(m=>m.Name==carType.Name);
        //    return View(carType);
        //}

        public void Delete(CarType carType)
        {
            _context.CarTypes.Remove(carType);
            Save();
        }

        public List<CarType> GetAll()
        {
            return _context.CarTypes.ToList();
        }

        public CarType GetById(Guid id)
        {
            return _context.CarTypes.FirstOrDefault(s=>s.Id==id);
        }

        public void Insert(CarType carType)
        {
            _context.CarTypes.Add(carType);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(CarType carType)
        {
            _context.CarTypes.Update(carType);
            Save();
        }
    }
}
