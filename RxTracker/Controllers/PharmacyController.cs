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
using RxTracker.ViewModels.Pharmacy;

namespace RxTracker.Controllers
{
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public PharmacyController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pharmacy
        public ActionResult Index()
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var model = new ListViewModel
            {
                PharmacyList = _context.Pharmacy
                    .Where(p => p.User == user)
                    .Select(p => new PharmacyListItem
                    {
                        PharmacyId = p.PharmacyId,
                        Name = p.Name,
                        Address = p.Address
                    })
                    .OrderBy(p => p.Name)
                    .ToList()
            };
            return View(model);
        }

        // GET: Pharmacy/Details/5
        public ActionResult Details(int id)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Pharmacy pharmacy = _context.Pharmacy.Find(id);
            if (pharmacy != null && pharmacy.User != user)
            {
                return Unauthorized();
            }
            if (pharmacy == null)
            {
                pharmacy = new Pharmacy();
            }
            return PartialView("_PharmacyPartial", pharmacy);
        }

        // POST: Pharmacy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pharmacy pharmacy)
        {
            // GET A REFERENCE TO THE USER
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (pharmacy.PharmacyId == 0)
            {
                pharmacy.User = user;
                _context.Pharmacy.Add(pharmacy);
            }
            else
            {
                EditPharmacy(pharmacy, user);
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

        // POST: Pharmacy/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody]int id)
        {
            Pharmacy pharmacyToDelete = _context.Pharmacy.Find(id);
            if (pharmacyToDelete == null)
            {
                return NoContent();
            }

            _context.Pharmacy.Remove(pharmacyToDelete);
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

        private bool EditPharmacy(Pharmacy pharmacy, MyUser user)
        {
            Pharmacy pharmacyToEdit = _context.Pharmacy.Find(pharmacy.PharmacyId);
            if (pharmacyToEdit == null || pharmacyToEdit.User != user)
            {
                return false;
            }

            pharmacyToEdit.Name = pharmacy.Name;
            pharmacyToEdit.Address = pharmacy.Address;
            return true;
        }
    }
}