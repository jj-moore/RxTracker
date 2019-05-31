using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;

namespace RxTracker.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DoctorController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Doctor
        public ActionResult Index()
        {

            var model = new ViewModels.Doctor.ListViewModel
            {
                DoctorList = _context.Doctor
                .Select(d => new ViewModels.Doctor.DoctorListItem
                {
                    Hospital = d.Hospital,
                    DoctorId = d.DoctorId,
                    Name = d.Name
                })
                .ToList()
            };

            return View(model);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            Doctor doctor = _context.Doctor.Find(id);
            if(doctor == null)
            {
                doctor = new Doctor();
            }
            return PartialView("_DoctorPartial", doctor);
        }
          
        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Doctor doctor)
        {

            if (doctor.DoctorId == 0)
            {
                _context.Doctor.Add(doctor);
            }
            else
            {
                EditDoctor(doctor);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete([FromBody]int id)
        {
            Doctor doctorToDelete = _context.Doctor.Find(id);
            if (doctorToDelete == null)
            {
                return NoContent();
            }

            _context.Doctor.Remove(doctorToDelete);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        private bool EditDoctor(Doctor doctor)
        {
            Doctor doctorToEdit = _context.Doctor.Find(doctor.DoctorId);
            if (doctorToEdit == null)
            {
                return false;
            }

            doctorToEdit.Name = doctor.Name;
            doctorToEdit.Hospital = doctor.Hospital;
            doctorToEdit.Address = doctor.Address;

            MyUser currentUser = _userManager.FindByNameAsync(this.User.Identity.Name).Result;
            doctorToEdit.User = currentUser;
            return true;
        }
    }
}