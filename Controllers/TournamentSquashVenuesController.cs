using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentAdminViewModels;
using SquashBotWebCore.Services;

namespace SquashBotWebCore.Views
{
    public class TournamentSquashVenuesController : Controller
    {
        private readonly ITournamentManagement _tournamentManagement;
        private readonly ITournamentSquashVenueManagement _tournamentSquashVenueManagement;


        public TournamentSquashVenuesController(ITournamentManagement tournamentManagement,
                                                ITournamentSquashVenueManagement tournamentSquashVenueManagement)
        {
            _tournamentManagement = tournamentManagement;
            _tournamentSquashVenueManagement = tournamentSquashVenueManagement;
        }

        // GET: TournamentSquashVenues
        public IActionResult Index(int tournamentId)
        {
            TournamentSquashVenueVm viewModel = new TournamentSquashVenueVm(_tournamentManagement.GetTournamentById(tournamentId));
            viewModel.SquashVenues = _tournamentSquashVenueManagement.GetTournamentSquashVenues(tournamentId);
            viewModel.AvailableSquashVenues = _tournamentSquashVenueManagement.GetAvailableSquashVenues(tournamentId);

            return View("Index", viewModel);
        }

        public IActionResult RemoveTournamentSquashVenue(int tournamentId, int squashVenueId)
        {
            //zb - validation goes here please
            _tournamentSquashVenueManagement.RemoveTournamentSquashVenue(tournamentId, squashVenueId);
            return RedirectToAction("Index", new { tournamentId });
        }

        public IActionResult AddTournamentSquashVenue(int tournamentId, int squashVenueId)
        {
            _tournamentSquashVenueManagement.AddTournamentSquashVenue(tournamentId, squashVenueId);
            return RedirectToAction("Index", new { tournamentId });
        }
    }
}
