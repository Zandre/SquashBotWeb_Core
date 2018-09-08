using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Controllers
{
    [Authorize]
    public class TournamentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITournamentManagement _tournamentManagement;
        private readonly ISectionManagement _sectionManagement;


        public TournamentsController(ApplicationDbContext context, 
                                        UserManager<ApplicationUser> userManager, 
                                        ITournamentManagement tournamentManagement,
                                        ISectionManagement sectionManagement)
        {
            _context = context;
            _userManager = userManager;
            _tournamentManagement = tournamentManagement;
            _sectionManagement = sectionManagement;
        }

        // GET: Tournament
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(_tournamentManagement.GetTournamentsForAdminUser(currentUser));
        }

        // GET: Tournament/SaveTournament
        public IActionResult SaveTournament()
        {
            TournamentVm viewModel = new TournamentVm();
            return View("TournamentAdminForm", viewModel);
        }

        // POST: Tournament/SaveTournament
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTournament([Bind("TournamentId,Name,SectionType,StartDate,EndDate")] TournamentVm viewModel)
        {
            TournamentVmValidator validator = new TournamentVmValidator();
            ValidationResult validationResult = validator.Validate(viewModel);

            if (validationResult.IsValid)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                _tournamentManagement.UpdateTournament(viewModel.Tournament(), currentUser);  
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName,
                                                failure.ErrorMessage);
                }
            }

            return View("TournamentAdminForm", viewModel);
        }

        // GET: Tournament/SaveTeam/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var tournament = _tournamentManagement.GetTournamentById(id);
            TournamentVm viewModel = new TournamentVm(tournament)
            {
                Sections = _sectionManagement.GetSectionsForTournament(tournament.TournamentId)
            };

            return View("TournamentAdminForm", viewModel);
        }

        // GET: Sections/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            _tournamentManagement.DeleteTournament(id);
            return RedirectToAction("Index");
        }
    }
}
