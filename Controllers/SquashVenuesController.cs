using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.FluentValidation;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Views
{
    public class SquashVenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISquashVenueManagement _squashVenueManagement;

        public SquashVenuesController(ApplicationDbContext context,
                                        ISquashVenueManagement squashVenueManagement)
        {
            _context = context;
            _squashVenueManagement = squashVenueManagement;
        }

        // GET: SquashVenues
        public async Task<IActionResult> Index()
        {
            return View(await _context.SquashVenues.ToListAsync());
        }


        // GET: SquashVenues/SaveSquashVenue
        public IActionResult SaveSquashVenue()
        {
           SquashVenueVm viewModel = new SquashVenueVm();
            return View("SquashVenueForm", viewModel);
        }

        // POST: SquashVenues/SaveSquashVenue
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSquashVenue([Bind("SquashVenueId,Name,CourtsAvailable")] SquashVenueVm viewModel)
        {
            SquashVenueVmValidator validator = new SquashVenueVmValidator();
            ValidationResult validationResult = validator.Validate(viewModel);

            if (validationResult.IsValid)
            {
                _squashVenueManagement.UpdateSquashVenue(viewModel.SquashVenue());
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName,
                                            failure.ErrorMessage);
                }
            }

            return View("SquashVenueForm", viewModel);
        }


        // POST: SquashVenues/SaveFixture/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var squashVenue = _squashVenueManagement.GetSquashVenueById(id);
            if (squashVenue == null)
            {
                return NotFound();
            }

            SquashVenueVm viewModel = new SquashVenueVm(squashVenue);
            return View("SquashVenueForm", viewModel);
        }

        // GET: SquashVenues/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            _squashVenueManagement.DeleteSquashVenue(id);

            return RedirectToAction("Index", "Home", null, null);
        }
    }
}
