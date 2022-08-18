using Rento.Models;
using System;
using System.Collections.Generic;

namespace Rento.Services.Interfaces
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCart> GetAll();
        ShoppingCart GetById(Guid id);
        void Insert(ShoppingCart shoppingCart);
        void Delete(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        void Save();
    }
}
