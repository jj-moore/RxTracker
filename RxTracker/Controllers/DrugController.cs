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
using RxTracker.ViewModels.Drug;

namespace RxTracker.Controllers
{
    public class DrugController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DrugController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Drug
        public ActionResult Index()
        {
            ListViewModel model = new ListViewModel
            {
                DrugList = _context.Drug
                    .Select(d => new DrugListItem
                    {
                        DrugId = d.DrugId,
                        Name = d.Name,
                        TradeName = d.TradeName
                    })
                    .OrderBy(d => d.Name)
                    .ToList()
            };

            return View(model);
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            Drug drug = _context.Drug.Find(id);
            if (drug == null)
            {
                drug = new Drug();
            }
            DetailViewModel model = new DetailViewModel
            {
                Drug = drug,
                GenericFor = _context.Drug
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                    })
                    .OrderBy(d => d.Text)
                    .ToList()
            };
            return PartialView("_DrugPartial", model);
        }

        // POST: Drug/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drug drug)
        {
            if (drug.DrugId == 0)
            {
                _context.Drug.Add(drug);
            }
            else
            {
                EditDrug(drug);
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

        // POST: Drug/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody]int id)
        {
            Drug drugToDelete = _context.Drug.Find(id);
            if (drugToDelete == null)
            {
                return NoContent();
            }

            _context.Drug.Remove(drugToDelete);
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

        private bool EditDrug(Drug drug)
        {
            Drug drugToEdit = _context.Drug.Find(drug.DrugId);
            if (drugToEdit == null)
            {
                return false;
            }

            drugToEdit.Name = drug.Name;
            drugToEdit.TradeName = drug.TradeName;
            drugToEdit.Manufacturer = drug.Manufacturer;
            drugToEdit.GenericForId = drug.GenericForId;

            MyUser currentUser = _userManager.FindByNameAsync(this.User.Identity.Name).Result;
            drugToEdit.User = currentUser;
            return true;
        }
    }
}