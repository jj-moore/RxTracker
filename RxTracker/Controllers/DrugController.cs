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
using RxTracker.ViewModels.Drug;

namespace RxTracker.Controllers
{
    [Authorize]
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
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ListViewModel model = new ListViewModel
            {
                DrugList = _context.Drug
                    .Where(d => d.User == user)
                    .Select(d => new DrugListItem
                    {
                        DrugId = d.DrugId,
                        DisplayName = d.DisplayName
                    })
                    .OrderBy(d => d.DisplayName)
                    .ToList()
            };
            return View(model);
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Drug drug = _context.Drug.Find(id);
            if (drug != null && drug.User != user)
            {
                return Unauthorized();
            }

            if (drug == null)
            {
                drug = new Drug();
            }

            DetailViewModel model = new DetailViewModel
            {
                Drug = drug,
                GenericFor = _context.Drug
                    .Where(d => d.User == user && !string.IsNullOrEmpty(d.TradeName))
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = d.DisplayName
                    })
                    .OrderBy(d => d.Text)
                    .ToList()
            };
            model.GenericFor.Insert(0, new SelectHelper
            {
                Value = 0,
                Text = ""
            });
            return PartialView("_DrugPartial", model);
        }

        // POST: Drug/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drug drug)
        {
            drug.GenericForId = drug.GenericForId == 0 ? null : drug.GenericForId;
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (drug.DrugId == 0)
            {
                drug.User = user;
                _context.Drug.Add(drug);
            }
            else
            {
                EditDrug(drug, user);
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

        private bool EditDrug(Drug drug, MyUser user)
        {
            Drug drugToEdit = _context.Drug.Find(drug.DrugId);
            if (drugToEdit == null || drugToEdit.User != user)
            {
                return false;
            }

            drugToEdit.Name = drug.Name;
            drugToEdit.TradeName = drug.TradeName;
            drugToEdit.Manufacturer = drug.Manufacturer;
            drugToEdit.GenericForId = drug.GenericForId;
            return true;
        }
    }
}