using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Controllers
{
    public class TournamentAdministratorsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITournamentAdministratorManagement _tournamentAdministratorManagement;
        private readonly ITournamentManagement _tournamentManagement;

        public TournamentAdministratorsController(UserManager<ApplicationUser> userManager,
                                                    ITournamentAdministratorManagement tournamentAdministratorManagement,
                                                    ITournamentManagement tournamentManagement)
        {
            _userManager = userManager;
            _tournamentAdministratorManagement = tournamentAdministratorManagement;
            _tournamentManagement = tournamentManagement;
        }

        public IActionResult Index(int tournamentId)
        {
            TournamentAdministratorVm viewModel = new TournamentAdministratorVm(_tournamentManagement.GetTournamentById(tournamentId));

            viewModel.AdminUsers = _tournamentAdministratorManagement.GetTournamentAdministrators(tournamentId);
            viewModel.AvailableUsers = _tournamentAdministratorManagement.GetAvailableUsers(tournamentId, viewModel.AdminUsers);

            return View("Index", viewModel);
        }

        public IActionResult RemoveAdminUser(int tournamentId, string userId)
        {
            //This is messy ZB... clean up your room
            TournamentAdministratorVm viewModel = new TournamentAdministratorVm(_tournamentManagement.GetTournamentById(tournamentId));

            viewModel.AdminUsers = _tournamentAdministratorManagement.GetTournamentAdministrators(tournamentId);
            viewModel.AvailableUsers = _tournamentAdministratorManagement.GetAvailableUsers(tournamentId, viewModel.AdminUsers);

            RemoveTournamentAdministratorVmValidator validator = new RemoveTournamentAdministratorVmValidator();
            ValidationResult validationResult = validator.Validate(viewModel);
            if (validationResult.IsValid)
            {
                var user = _userManager.FindByIdAsync(userId);
                _tournamentManagement.RemoveTournamentAdministrator(tournamentId, user.Result);
            }
            else
            {
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ModelState.AddModelError("AdminUsers",
                                            failure.ErrorMessage);
                }
            }

            return RedirectToAction("Index", new { tournamentId });
        }

        public IActionResult AddAdminUser(int tournamentId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            _tournamentManagement.AddTournamentAdministrator(tournamentId, user.Result);

            return RedirectToAction("Index", new { tournamentId });
        }
    }
}