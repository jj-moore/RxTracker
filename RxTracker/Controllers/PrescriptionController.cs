using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using RxTracker.ViewModels.Prescription;
using System.Linq;

namespace RxTracker.Controllers
{
    [Authorize]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public PrescriptionController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Prescription
        public ActionResult Index()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ListViewModel model = new ListViewModel
            {
                PrescriptionList = _context.Prescription
                    .Where(p => p.Active && p.User == user)
                    .Include(p => p.Drug)
                    .Include(p => p.Doctor)
                    .Select(p => new PrescriptionListItem
                    {
                        PrescriptionId = p.PrescriptionId,
                        DrugDisplayName = p.Drug.DisplayName,
                        DoctorName = p.Doctor.Name
                    }).ToList()
            };
            return View(model);
        }

        // GET: Prescription/Details/5
        public ActionResult Details(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Prescription prescription = _context.Prescription.Find(id);
            if (prescription != null && prescription.User != user)
            {
                return Unauthorized();
            }
            if (prescription == null)
            {
                prescription = new Prescription();
            }

            EditViewModel model = new EditViewModel
            {
                Prescription = prescription,
                Doctors = _context.Doctor
                    .Where(d => d.User == user)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DoctorId,
                        Text = d.Name
                    })
               .ToList(),
                Drugs = _context.Drug
                    .Where(d => d.User == user)
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                    })
                   .ToList(),
            };
            model.Doctors.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });
            model.Drugs.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });

            return PartialView("_PrescriptionPartial", model);
        }

        // POST: Prescription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prescription prescription)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (prescription.PrescriptionId == 0)
            {
                prescription.User = user;
                _context.Prescription.Add(prescription);
            }
            else
            {
                EditPrescription(prescription, user);
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
      
        // POST: Prescription/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Prescription prescriptionToDelete = _context.Prescription.Find(id);
            if (prescriptionToDelete == null)
            {
                return NoContent();
            }

            _context.Prescription.Remove(prescriptionToDelete);
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

        private bool EditPrescription(Prescription prescription, MyUser user)
        {
            Prescription prescriptionToEdit = _context.Prescription.Find(prescription.PrescriptionId);
            if (prescriptionToEdit == null || prescriptionToEdit.User != user)
            {
                return false;
            }

            prescriptionToEdit.DoctorId = prescription.DoctorId;
            prescriptionToEdit.DrugId = prescription.DrugId;
            prescriptionToEdit.Active = prescription.Active;
            prescriptionToEdit.Form = prescription.Form;
            prescriptionToEdit.Dosage = prescription.Dosage;
            prescriptionToEdit.Regimen = prescription.Regimen;
            return true;
        }
    }
}