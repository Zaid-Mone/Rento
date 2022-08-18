using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rento.Data;
using Rento.Models;
using Rento.Services.Interfaces;

namespace Rento.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarRepository _carRepo;
        private readonly IWebHostEnvironment _hosting;

        public CarController(ApplicationDbContext context
            , ICarRepository carRepo
            , IWebHostEnvironment hosting)
        {
            _context = context;
            this._carRepo = carRepo;
            this._hosting = hosting;
        }

        // GET: Admin/Car
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _carRepo.GetAll();
            return View(applicationDbContext);
        }

        // GET: Admin/Car/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Admin/Car/Create
        public IActionResult Create()
        {
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Color,Year,Model,PlateNumber,PricePerDay,CarTypeId")] Car car)
        {
            if (ModelState.IsValid)
            {
                var IsExist = _context.Cars.Any(n => n.Name == car.Name);
                if (IsExist)
                {
                    ViewBag.msg = "The Car Name is Already Exist";
                    return View(car);
                }

                car.Id = Guid.NewGuid();
                UploadImage(car);
                _carRepo.Insert(car);
                _carRepo.Save();
                TempData["save"] = "Car Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // GET: Admin/Car/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // POST: Admin/Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ImageUrl,Color,Year,Model,PlateNumber,PricePerDay,CarTypeId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var IsExist = _context.Cars.Any(n => n.Name == car.Name);
                    if (IsExist)
                    {
                        ViewBag.msg = "The Car Name is Already Exist";
                        return View(car);
                    }
                    UploadImage(car);
                    _carRepo.Update(car);
                    _carRepo.Save();
                    TempData["edit"] = "Car Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // GET: Admin/Car/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carRepo.GetById(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Admin/Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            _carRepo.Delete(car);
            _carRepo.Save();
            TempData["delete"] = "Car Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        private void UploadImage(Car model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {//@"wwwroot/"
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(_hosting.WebRootPath, "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.ImageUrl = ImageName;
            }
            else if (model.ImageUrl == null && model.Id == null)
            {
                model.ImageUrl = "NoImage.png";
            }
            else
            {
                model.ImageUrl = model.ImageUrl;
            }
        }

    }
}
