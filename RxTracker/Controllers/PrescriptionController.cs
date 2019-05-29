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
        private readonly UserManager<IdentityUser> _userManager;

        public PrescriptionController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Prescription
        public ActionResult Index()
        {
            ListViewModel model = new ListViewModel
            {
                PrescriptionList = _context.Prescription
                .Where(p => p.Active)
                .Include(p => p.Drug)
                .Select(p => new PrescriptionList
                {
                    PrescriptionId = p.PrescriptionId,
                    DrugName = p.Drug.Name,
                    TradeName = p.Drug.TradeName
                }).ToList()
            };
            return View(model);
        }

        // GET: Prescription/Details/5
        public ActionResult Details(int id)
        {
            Prescription prescription = _context.Prescription.Find(id);
            if (prescription == null)
            {
                return NoContent();
            }

            EditViewModel model = new EditViewModel
            {
                Prescription = prescription,
                Doctors = _context.Doctor.Select(d => new SelectHelper
                {
                    Value = d.DoctorId,
                    Text = d.Name
                })
               .ToList(),
                Drugs = _context.Drug.Select(d => new SelectHelper
                {
                    Value = d.DrugId,
                    Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                })
               .ToList(),
            };

            return PartialView("_PrescriptionPartial", model);
        }

        // GET: Prescription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prescription/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Prescription/Edit/5
        public ActionResult Edit(int id)
        {
            EditViewModel model = new EditViewModel
            {
                Prescription = _context.Prescription.Find(id),
                Doctors = _context.Doctor.Select(d => new SelectHelper
                {
                    Value = d.DoctorId,
                    Text = d.Name
                })
               .ToList(),
                Drugs = _context.Drug.Select(d => new SelectHelper
                {
                    Value = d.DrugId,
                    Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                })
               .ToList(),
            };

            return View(model);
        }

        // POST: Prescription/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Prescription prescription)
        {
            try
            {
                var prescriptionToEdit = _context.Prescription.Find(prescription.PrescriptionId);
                if (prescriptionToEdit == null)
                    return StatusCode(400);

                prescriptionToEdit.DoctorId = prescription.DoctorId;
                prescriptionToEdit.DrugId = prescription.DrugId;
                prescriptionToEdit.Active = prescription.Active;
                prescriptionToEdit.Form = prescription.Form;
                prescriptionToEdit.Dosage = prescription.Dosage;
                prescriptionToEdit.Regimen = prescription.Regimen;

                IdentityUser currentUser =_userManager.FindByNameAsync(this.User.Identity.Name).Result;
                prescriptionToEdit.IdentityUser = currentUser;

                _context.SaveChanges();
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }

        // GET: Prescription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Prescription/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}