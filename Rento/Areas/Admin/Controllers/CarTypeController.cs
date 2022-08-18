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

namespace Rento.Areas.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CarTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarTypeRepository _carTypeRepo;
        private readonly IWebHostEnvironment _hosting;

        public CarTypeController(ApplicationDbContext context
            , ICarTypeRepository carTypeRepo
            , IWebHostEnvironment hosting)
        {
            _context = context;
            this._carTypeRepo = carTypeRepo;
            this._hosting = hosting;
        }

        // GET: Admin/CarType
        public async Task<IActionResult> Index()
        {
            return View(_carTypeRepo.GetAll());
        }

        // GET: Admin/CarType/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = _carTypeRepo.GetById(id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: Admin/CarType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CarType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl")] CarType carType)
        {
            if (ModelState.IsValid)
            {
                var IsExist = _context.CarTypes.Any(n => n.Name == carType.Name);
                if (IsExist)
                {
                    ViewBag.msg = "The Car Name is Already Exist";
                    return View(carType);
                }
           
                carType.Id = Guid.NewGuid();
                UploadImage(carType);
                _carTypeRepo.Insert(carType);
                _carTypeRepo.Save();
                TempData["save"] = "CarType Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(carType);
        }

        // GET: Admin/CarType/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = _carTypeRepo.GetById(id);
            if (carType == null)
            {
                return NotFound();
            }
            return View(carType);
        }

        // POST: Admin/CarType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ImageUrl")] CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var IsExist = _context.CarTypes.Any(n => n.Name == carType.Name);
                    //if (IsExist)
                    //{
                    //    ViewBag.msg = "The Car Name is Already Exist";
                    //    return View(carType);
                    //}
                    UploadImage(carType);
                    _carTypeRepo.Update(carType);
                    _carTypeRepo.Save();
                    TempData["edit"] = "CarType Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarTypeExists(carType.Id))
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
            return View(carType);
        }

        // GET: Admin/CarType/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = _carTypeRepo.GetById(id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // POST: Admin/CarType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carType = _carTypeRepo.GetById(id);
            _carTypeRepo.Delete(carType);
            _carTypeRepo.Save();
            TempData["delete"] = "CarType Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

        private bool CarTypeExists(Guid id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }



        private void UploadImage(CarType model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {//@"wwwroot/"
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(_hosting.WebRootPath, "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.ImageUrl = ImageName;
            }
            else if (model.ImageUrl == null)
            {
                model.ImageUrl = "9a413f45-a9ef-41fe-9b61-aa055c83d22e.png";
            }
            else
            {
                model.ImageUrl = model.ImageUrl;
            }
        }

    }
}
