using Rento.Models;
using System;
using System.Collections.Generic;

namespace Rento.Services.Interfaces
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car GetById(Guid id);
        void Insert(Car car);
        void Delete(Car car);
        void Update(Car car);
        void Save();
    }
}
