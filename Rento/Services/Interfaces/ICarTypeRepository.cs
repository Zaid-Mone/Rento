using Microsoft.AspNetCore.Mvc;
using Rento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.Services.Interfaces
{
    public interface ICarTypeRepository
    {
        List<CarType> GetAll();
        CarType GetById(Guid id);
        void Insert(CarType carType);
        void Delete(CarType carType);
        void Update(CarType carType); 
        void Save();
        //CarType Check(CarType carType);
        //void CheckExist(CarType carType);
        //public IActionResult Check(CarType carType);
    }
}
