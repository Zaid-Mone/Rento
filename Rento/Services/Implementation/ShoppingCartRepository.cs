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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Delete(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            Save();
        }

        public List<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts
                .Include(n => n.ApplicationUser)
                .Include(b => b.Car)
                .ThenInclude(c => c.CarType)
                .ToList();
        }

        public ShoppingCart GetById(Guid id)
        {
            return _context.ShoppingCarts
               .Include(n => n.ApplicationUser)
               .Include(b => b.Car)
               .ThenInclude(c => c.CarType)
               .FirstOrDefault(y => y.Id == id);
        }

        public void Insert(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            Save();
        }
    }
}
