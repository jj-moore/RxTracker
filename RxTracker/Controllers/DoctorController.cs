using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;

namespace RxTracker.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DoctorController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// This is the page for doing CRUD operations on doctors. The user is
        /// presented with a list of doctors. Selecting a doctor will display the
        /// editable details.
        /// </summary>
        /// <returns>A stream of html representing the web page</returns>
        public ActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var model = new ViewModels.Doctor.ListViewModel
            {
                DoctorList = _context.Doctor
                .Where(d => d.User == user)
                .Select(d => new ViewModels.Doctor.DoctorListItem
                {
                    DoctorId = d.DoctorId,
                    Hospital = d.Hospital,
                    Name = d.Name
                })
                .ToList()
            };

            return View(model);
        }

        /// <summary>
        /// Called by JavaScript to render the partial page which displays the details
        /// of the selected doctor. This partial page is inserted into the website.
        /// </summary>
        /// <param name="id">The primary key of the doctor for whom details are being
        /// requested. A check is made to make sure the current user is listed as the 
        /// user on this doctor's record.
        /// </param>
        /// <returns>A stream of html representing the partial page showing details.</returns>
        public ActionResult Details(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Doctor doctor = _context.Doctor.Find(id);
            if (doctor != null && doctor.User != user)
            {
                return Unauthorized();
            }
            if (doctor == null)
            {
                doctor = new Doctor();
            }
            return PartialView("_DoctorPartial", doctor);
        }

        [HttpPost]
        public ActionResult Edit([FromBody]Doctor doctor)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (doctor.DoctorId == 0)
            {
                doctor.User = user;
                _context.Doctor.Add(doctor);
            }
            else
            {
                EditDoctor(doctor, user);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

            return Json(doctor.DoctorId);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
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

        private bool EditDoctor(Doctor doctor, MyUser user)
        {
            Doctor doctorToEdit = _context.Doctor.Find(doctor.DoctorId);
            if (doctorToEdit == null || doctorToEdit.User != user)
            {
                return false;
            }

            doctorToEdit.Name = doctor.Name;
            doctorToEdit.Hospital = doctor.Hospital;
            doctorToEdit.Address = doctor.Address;
            return true;
        }
    }
}