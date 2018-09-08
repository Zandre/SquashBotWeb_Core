using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Services
{

    public interface ITournamentSquashVenueManagement
    {
        TournamentSquashVenue GetTournamentSquashVenueById(int id);
        void AddTournamentSquashVenue(int tournamentId, int squashVenueId);
        void RemoveTournamentSquashVenue(int tournamentId, int squashVenueId);
        List<SquashVenue> GetTournamentSquashVenues(int tournamentId);
        List<SquashVenue> GetAvailableSquashVenues(int tournamentId);
        List<TournamentSquashVenue> GetAvailableTournamentSquashVenues(int tournamentId);
    }

    public class TournamentSquashVenueManagement : ITournamentSquashVenueManagement
    {
        private readonly ApplicationDbContext _context;
        private readonly ITournamentManagement _tournamentManagement;
        private readonly ISquashVenueManagement _squashVenueManagement;

        public TournamentSquashVenueManagement(ApplicationDbContext context,
                                                ITournamentManagement tournamentManagement,
                                                ISquashVenueManagement squashVenueManagement)
        {
            _context = context;
            _tournamentManagement = tournamentManagement;
            _squashVenueManagement = squashVenueManagement;
        }

        public TournamentSquashVenue GetTournamentSquashVenueById(int id)
        {
            TournamentSquashVenue tsv =
                _context.TournamentSquashVenues.SingleOrDefault(t => t.TournamentSquashVenueId == id);

            tsv.SquashVenue = _squashVenueManagement.GetSquashVenueById(tsv.SquashVenueId);
            return tsv;

        }

        public void AddTournamentSquashVenue(int tournamentId, int squashVenueId)
        {
            Tournament t = _tournamentManagement.GetTournamentById(tournamentId);
            SquashVenue sv = _squashVenueManagement.GetSquashVenueById(squashVenueId);
            TournamentSquashVenue tournamentSquashVenue = new TournamentSquashVenue()
            {
                Tournament = t,
                SquashVenue = sv
            };

            _context.Add(tournamentSquashVenue);
            _context.SaveChanges();
        }

        public void RemoveTournamentSquashVenue(int tournamentId, int squashVenueId)
        {
            TournamentSquashVenue tournamentSquashVenue = _context.TournamentSquashVenues
                .SingleOrDefault(x => x.TournamentId == tournamentId &&
                                      x.SquashVenueId == squashVenueId);

            _context.TournamentSquashVenues.Remove(tournamentSquashVenue);
            _context.SaveChangesAsync();
        }

        public List<SquashVenue> GetTournamentSquashVenues(int tournamentId)
        {
            List<SquashVenue> squashVenues = _context.TournamentSquashVenues
                                                                .Where(tsv => tsv.TournamentId == tournamentId)
                                                                .Select(x => x.SquashVenue)
                                                                .ToList();

            return squashVenues;
        }

        public List<SquashVenue> GetAvailableSquashVenues(int tournamentId)
        {
            var usedSquashVenues = GetTournamentSquashVenues(tournamentId);

            List<SquashVenue> availableSquashVenues = new List<SquashVenue>();
            foreach (var squashVenue in _context.SquashVenues.ToList())
            {
                if (!usedSquashVenues.Contains(squashVenue))
                {
                    availableSquashVenues.Add(squashVenue);
                }
            }
            
            return availableSquashVenues;
        }

        public List<TournamentSquashVenue> GetAvailableTournamentSquashVenues(int tournamentId)
        {
            List<TournamentSquashVenue> squashVenues = _context.TournamentSquashVenues
                                                                .Where(tsv => tsv.TournamentId == tournamentId)
                                                                .Include(x => x.SquashVenue)
                                                                .ToList();


            return squashVenues;
        }
    }
}
