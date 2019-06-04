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
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // GET A LIST OF ALL DOCTORS FOR THIS USER AND CREATE A LIST OF PERTINENT DETAILS
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
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // FIND THE DOCTOR BY ID
            Doctor doctor = _context.Doctor.Find(id);

            // CONFIRM CURRENT USER HAS ACCESS TO THIS DOCTOR
            if (doctor != null && doctor.User != user)
            {
                return Unauthorized();
            }

            // IF DOCTOR NOT FOUND THEN RETURN AN EMPTY DOCTOR OBJECT
            // THIS WILL BE USED WHEN ADDING A NEW DOCTOR
            if (doctor == null)
            {
                doctor = new Doctor();
            }
            return PartialView("_DoctorPartial", doctor);
        }

        /// <summary>
        /// The details of a doctor are received from the front end and persisted into the
        /// database. If the DoctorId = 0, this indicates a new doctor is being added, otherwise
        /// we attempt to find the doctor by id and change his details to match the details received.
        /// So this method handles both create and update. Note the user associated with the 
        /// doctor is assigned by the backend and is not modified once assigned.
        /// </summary>
        /// <param name="doctor">The details received by the frontend mapped into a Doctor object</param>
        /// <returns>The id of the doctor added or edited or an error if the doctor could not be found
        /// or there is a database error
        /// </returns>
        [HttpPost]
        public ActionResult Edit([FromBody]Doctor doctor)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // IF DOCTOR ID EQUALS ZERO THEN INSERT THE DOCTOR INTO THE DATABASE
            if (doctor.DoctorId == 0)
            {
                doctor.User = user;
                _context.Doctor.Add(doctor);
            }
            // ELSE CALL EditDoctor TO EDIT THE DOCTORS DETAILS
            else
            {
                EditDoctor(doctor, user);
            }

            // MAKE THE DATABASE INSERT OR UPDATE
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
                // TODO: Handle the error on the front end
            }

            return Json(doctor.DoctorId);
        }

        /// <summary>
        /// Receives a DoctorId from the front end. If the doctor is found first the
        /// user assigned to the doctor is compared to the current request to make sure
        /// they match. If the do the doctor is deleted from the database, otherwise
        /// an unauthorized reponse is returned.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete([FromBody]int id)
        {
            Doctor doctorToDelete = _context.Doctor.Find(id);
            if (doctorToDelete == null)
            {
                return NoContent();
            }

            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            // CONFIRM THE CURRENT USER MATCHES THE USER ASSIGNED TO THIS DOCTOR
            if (doctorToDelete.User != user)
            {
                return Unauthorized();
                //TODO: Handle the unauthorized reponse 
            }

            // REMOVE THE DOCTOR FROM THE DATABASE
            _context.Doctor.Remove(doctorToDelete);

            // MAKE THE DATABASE CALL TO DELETE
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
                // TODO: Handle the error on the front end
            }

            return Ok();
        }


        /// <summary>
        /// Helper method to make controller logic clearer. Given a doctor and a user
        /// this method will find the doctor in the database, confirm the user
        /// has access to it and update the doctor. The call to update the database
        /// must be made separately.
        /// </summary>
        /// <param name="doctor">The information about the doctor to edit</param>
        /// <param name="user">The current request user trying to edit the doctor</param>
        /// <returns></returns>
        private bool EditDoctor(Doctor doctor, MyUser user)
        {
            // GET THE DOCTOR FROM THE DATABASE
            Doctor doctorToEdit = _context.Doctor.Find(doctor.DoctorId);

            // RETURN FALSE IF DOCTOR CANNOT BE FOUND OR USER REFERENCED
            // BY THIS DOCTOR DOES NOT MATCH THE PARAMETER
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