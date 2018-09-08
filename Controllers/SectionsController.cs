using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.FluentValidation;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Models.TournamentViewerViewModel;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Controllers
{
    [Authorize]
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITournamentManagement _tournamentManagement;
        private readonly ISectionManagement _sectionManagement;
        private readonly ITeamManagement _teamManagement;
        private readonly IFixtureManagement _fixtureManagement;

        public SectionsController(ApplicationDbContext context,
                                    ITournamentManagement _tournamentManagement,
                                    ISectionManagement sectionManagement,
                                    ITeamManagement teamManagement,
                                    IFixtureManagement fixtureManagement)
        {
            _context = context;
            this._tournamentManagement = _tournamentManagement;
            _sectionManagement = sectionManagement;
            _teamManagement = teamManagement;
            _fixtureManagement = fixtureManagement;
        }

        // GET: Sections/SaveSection
        public IActionResult SaveSection(int tournamentId)
        {
            SectionVm viewModel = new SectionVm();
            Tournament tournament = _tournamentManagement.GetTournamentById(tournamentId);
            viewModel.TournamentId = tournament.TournamentId;
            viewModel.Tournament = tournament;
            return View("SectionAdminForm", viewModel);
        }

        [AllowAnonymous]
        public IActionResult SectionsDashboard(int id)
        {
            SectionUserViewVm viewModel = new SectionUserViewVm(_tournamentManagement.GetTournamentById(id));
            viewModel.MensSections = _sectionManagement.GetMenSectionsForTournament(id);
            foreach (var mensSection in viewModel.MensSections)
            {
                mensSection.Teams = _teamManagement.GetTeamsForSection(mensSection.SectionId);
                mensSection.Fixtures = _fixtureManagement.GetFixturesForSection(mensSection.SectionId);
            }
            viewModel.WomensSections = _sectionManagement.GetWomenSectionsForTournament(id);
            foreach (var womensSection in viewModel.WomensSections)
            {
                womensSection.Teams = _teamManagement.GetTeamsForSection(womensSection.SectionId);
                womensSection.Fixtures = _fixtureManagement.GetFixturesForSection(womensSection.SectionId);
            }
            viewModel.MixedSections = _sectionManagement.GetMixedSectionsForTournament(id);
            foreach (var mixedSection in viewModel.MixedSections)
            {
                mixedSection.Teams = _teamManagement.GetTeamsForSection(mixedSection.SectionId);
                mixedSection.Fixtures = _fixtureManagement.GetFixturesForSection(mixedSection.SectionId);
            }
            return View("SectionUserView", viewModel);
        }

        [AllowAnonymous]
        public IActionResult LogAndFixtures(int id)
        {
            SectionDetailsVm viewModel = new SectionDetailsVm(_sectionManagement.GetSectionById(id));
            viewModel.Section.Tournament = _tournamentManagement.GetTournamentBySectionId(viewModel.SectionId);
            viewModel.Teams = _teamManagement.GetTeamsForSection(id);
            viewModel.Fixtures = _fixtureManagement.GetFixturesForSection(id);

            return View("SectionLogAndFixtureDetailsView", viewModel);
        }

        // POST: Sections/SaveSection
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSection([Bind("SectionId,TournamentId,Name,PlayersPerTeam,Par,Gender")] SectionVm viewModel)
        {
            SectionVmValidator validator = new SectionVmValidator();
            ValidationResult validationResult = validator.Validate(viewModel);

            if (validationResult.IsValid)
            {
                _sectionManagement.UpdateSection(viewModel.Section());
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName,
                                            failure.ErrorMessage);
                }
            }

            viewModel.Tournament = _tournamentManagement.GetTournamentById(viewModel.TournamentId);
            viewModel.Teams = _teamManagement.GetTeamsForSection(viewModel.SectionId);
            return View("SectionAdminForm", viewModel);
        }

        // GET: Section/SaveTeam/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var section = _sectionManagement.GetSectionById(id);

            SectionVm viewModel = new SectionVm(section)
            {
                Tournament = _tournamentManagement.GetTournamentById(section.TournamentId),
                Teams = _teamManagement.GetTeamsForSection(id)
            };

            return View("SectionAdminForm", viewModel);
        }

        // GET: Sections/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

           int tournamentId = _sectionManagement.DeleteSection(id);

            return RedirectToAction("SaveFixture", "Tournaments", new { id = tournamentId }, null);
        }
    }
}
