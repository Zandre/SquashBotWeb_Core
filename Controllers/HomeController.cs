using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SquashBotWebCore.Models;
using SquashBotWebCore.Models.TournamentViewerViewModel;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITournamentManagement _tournamentManagement;

        public HomeController(ITournamentManagement tournamentManagement)
        {
            _tournamentManagement = tournamentManagement;
        }

        public IActionResult Index()
        {
            TournamentDashboardVm viewModel = new TournamentDashboardVm();
            viewModel.ActiveTournaments = _tournamentManagement.GetActiveTournaments();
            viewModel.UpcomingTournaments = _tournamentManagement.GetUpcomingTournaments();
            viewModel.PastTournaments = _tournamentManagement.GetPastTournaments();

            return View(viewModel);
        }

        public IActionResult SectionsDashboard(int id)
        {
            return RedirectToAction("SectionsDashboard", "Sections", new { id = id });
        } 

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
