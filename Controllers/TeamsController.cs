using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SquashBotWebCore.Models.FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly ITeamManagement _teamManagement;
        private readonly ISectionManagement _sectionManagement;
        private readonly ITournamentManagement _tournamentManagement;

        public TeamsController(ITeamManagement teamManagement,
                                ISectionManagement sectionManagement,
                                ITournamentManagement tournamentManagement)
        {
            _teamManagement = teamManagement;
            _sectionManagement = sectionManagement;
            _tournamentManagement = tournamentManagement;
        }

        // GET: Teams/SaveTeam/5
        public IActionResult SaveTeam(int sectionId)
        {
            TeamVm viewModel = new TeamVm();
            var section = _sectionManagement.GetSectionById(sectionId);

            viewModel.Section = section;
            viewModel.SectionId = section.SectionId;
            viewModel.Section.Tournament = _tournamentManagement.GetTournamentById(section.TournamentId);
            
            return View("TeamAdminForm", viewModel);
        }

        // POST: Teams/SaveTeam/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTeam(int id, [Bind("TeamId,SectionId,Name")] TeamVm viewModel)
        {
            TeamVmValidator validator = new TeamVmValidator();
            ValidationResult validationResult = validator.Validate(viewModel);

            if (validationResult.IsValid)
            {
                _teamManagement.UpdateTeam(viewModel.Team());
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName,
                                            failure.ErrorMessage);
                }
            }

            var section = _sectionManagement.GetSectionById(viewModel.SectionId);
            viewModel.Section = section;
            viewModel.SectionId = section.SectionId;
            viewModel.Section.Tournament = _tournamentManagement.GetTournamentById(section.TournamentId);

            return View("TeamAdminForm", viewModel);
        }


        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var team = _teamManagement.GetTeamById(id);
            if (team == null)
            {
                return NotFound();
            }

            TeamVm viewModel = new TeamVm(team);
            var section = _sectionManagement.GetSectionById(team.SectionId);
            viewModel.SectionId = section.SectionId;
            viewModel.Section = section;
            viewModel.Section.Tournament = _tournamentManagement.GetTournamentById(section.TournamentId);

            return View("TeamAdminForm", viewModel);
        }

        // GET: Teams/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            int sectionId = _teamManagement.DeleteTeam(id);

            return RedirectToAction("SaveFixture", "Sections", new { id = sectionId }, null);
        }
    }
}
