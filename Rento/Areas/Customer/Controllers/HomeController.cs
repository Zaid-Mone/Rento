using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rento.Models;
using System.Diagnostics;
using Rento.ViewModels;
using Rento.Data;
using Rento.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rento.Utility;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;

namespace Rento.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICarRepository _carRepo;
        private readonly ICarTypeRepository _carTypeRepo;
        public HomeController(ILogger<HomeController> logger
            , ApplicationDbContext context
            , ICarRepository carRepo
           , ICarTypeRepository carTypeRepo)
        {
            _logger = logger;
            _context = context;
            _carRepo = carRepo;
            _carTypeRepo = carTypeRepo;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVm = new HomeViewModel
            {
                Cars=_carRepo.GetAll(),
                CarTypes=_carTypeRepo.GetAll()  //.ToPagedList(pageNumber: page ?? 1, pageSize: 3)
        };
            return View(homeVm);
        }    
        public IActionResult CarsHomePage(int? page)
        {
            var item = _context.Cars.ToList().ToPagedList(pageNumber: page ?? 1, pageSize: 2);

            //Cars = _context.Cars.ToList().ToPagedList(pageNumber: page ?? 1, pageSize: 3);
            //homeVm.CarTypes = _context.CarTypes.ToList().ToPagedList(pageNumber: page ?? 1, pageSize: 3);
                //.ToPagedList(pageNumber: page ?? 1, pageSize: 3)
          
            return View(item);
        }

        public IActionResult Cars(Guid id)
        {
            var carType = _carTypeRepo.GetById(id);
            if(carType == null)
            {
                return NotFound();
            }
            var cars = _context.Cars.Include(n => n.CarType).Where(o => o.CarTypeId==id).ToList();
            if (cars.Count() == 0)
            {
                ViewBag.message = "No Cars Added yet";
            }
            return View(cars);
        }


        public IActionResult Details(Guid id)
        {
            var car = _carRepo.GetById(id);
            if(car == null)
            {
                return NotFound();
            }
 
            return View(car);
        }

      public static List<Car> GetCars()
        {
            List<Car> CarList = new List<Car>();

            return (List<Car>)CarList.OrderBy(n=>n.Name).Distinct();
        }



        public IActionResult MoreDetails(Guid id)
        {
            var shoppingCart = new ShoppingCart
            {
                CarId=id,
                Car = _context.Cars.Include(n => n.CarType).FirstOrDefault(u => u.Id == id),
            };
            return View(shoppingCart);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _context.ShoppingCarts.FirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.Id == shoppingCart.Id);


            if (cartFromDb == null)
            {

                _context.ShoppingCarts.Add(shoppingCart);
                _context.SaveChanges();
                HttpContext.Session.SetInt32(WebRole.SessionCart,
                    _context.ShoppingCarts.Where(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                //_context.ShoppingCarts.IncrementCount(cartFromDb, shoppingCart.Count);
                _context.SaveChanges();
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
