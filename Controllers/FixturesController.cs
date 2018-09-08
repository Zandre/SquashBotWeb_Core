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
    public class FixturesController : Controller
    {
        private readonly IFixtureManagement _fixtureManagement;
        private readonly ISectionManagement _sectionManagement;
        private readonly ITeamManagement _teamManagement;
        private readonly ITournamentSquashVenueManagement _tournamentSquashVenueManagement;
        private readonly ITournamentManagement _tournamentManagement;

        public FixturesController(IFixtureManagement fixtureManagement,
                                    ISectionManagement sectionManagement,
                                    ITeamManagement teamManagement,
                                    ITournamentSquashVenueManagement tournamentSquashVenueManagement,
                                    ITournamentManagement tournamentManagement)
        {
            _fixtureManagement = fixtureManagement;
            _sectionManagement = sectionManagement;
            _teamManagement = teamManagement;
            _tournamentSquashVenueManagement = tournamentSquashVenueManagement;
            _tournamentManagement = tournamentManagement;
        }

        // GET: Fixtures
        public IActionResult Index(int sectionId)
        {
            var fixtureVms = _fixtureManagement.FixtureIndexViewModel(sectionId);
            return View(fixtureVms);
        }

        // GET: Fixtures/SaveFixture/5
        public IActionResult CreateNewFixture(int sectionId)
        {
            FixtureVm viewModel = new FixtureVm();            
            viewModel.SectionId = sectionId;
            viewModel.Section = _sectionManagement.GetSectionById(sectionId);
            viewModel.AvailableTeams = _teamManagement.GetTeamsForSection(sectionId);
            viewModel.AvailableVenues = _tournamentSquashVenueManagement.GetAvailableTournamentSquashVenues(viewModel.Section.TournamentId);

            return View("FixtureAdminForm", viewModel);
        }

        public IActionResult SaveFixture(int fixtureId)
        {
            var fixture = _fixtureManagement.GetFixtureById(fixtureId);
            FixtureVm viewModel = new FixtureVm(fixture);
            viewModel.TournamentSquashVenueId = fixture.TournamentSquashVenueId;
            viewModel.TournamentSquashVenue = _tournamentSquashVenueManagement.GetTournamentSquashVenueById(fixture.TournamentSquashVenueId);
            viewModel.Section = _sectionManagement.GetSectionById(viewModel.SectionId);
            viewModel.AvailableTeams = _teamManagement.GetTeamsForSection(viewModel.SectionId);
            viewModel.AvailableVenues = _tournamentSquashVenueManagement.GetAvailableTournamentSquashVenues(viewModel.Section.TournamentId);

            return View("FixtureAdminForm", viewModel);
        }

        // POST: Fixtures/SaveFixture/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFixture(int id, [Bind("FixtureId,TeamAId,TeamBId,SectionId,DateTime,TournamentSquashVenueId,Court")] FixtureVm viewModel)
        {
            var tournament = _tournamentManagement.GetTournamentBySectionId(viewModel.SectionId);
            var tournamentSquashVenue = _tournamentSquashVenueManagement.GetTournamentSquashVenueById(viewModel.TournamentSquashVenueId);
            FixtureVmValidator validator = new FixtureVmValidator(tournament, tournamentSquashVenue);
            ValidationResult validationResult = validator.Validate(viewModel);

            if (validationResult.IsValid)
            {
                _fixtureManagement.UpdateFixture(viewModel.Fixture());
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName,
                                            failure.ErrorMessage);
                }
            }

            viewModel.SectionId = viewModel.SectionId;
            viewModel.Section = _sectionManagement.GetSectionById(viewModel.SectionId);
            viewModel.AvailableTeams = _teamManagement.GetTeamsForSection(viewModel.SectionId);
            viewModel.AvailableVenues = _tournamentSquashVenueManagement.GetAvailableTournamentSquashVenues(viewModel.Section.TournamentId);

            return View("FixtureAdminForm", viewModel);
        }

        // GET: Fixtures/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            int sectionId = _fixtureManagement.DeleteFixture(id);

            return RedirectToAction("SaveFixture", "Sections", new { id = sectionId }, null);
        }
    }
}
