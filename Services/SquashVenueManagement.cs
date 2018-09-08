using System.Linq;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Services
{
    public interface ISquashVenueManagement
    {
        SquashVenue GetSquashVenueById(int squashVenueId);
        void DeleteSquashVenue(int id);
        void UpdateSquashVenue(SquashVenue squashVenue);
        void CreateNewSquashVenue(SquashVenue squashVenue);
        void UpdateExistingSquashVenue(SquashVenue squashVenue);

    }

    public class SquashVenueManagement: ISquashVenueManagement
    {
        private readonly ApplicationDbContext _context;

        public SquashVenueManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public SquashVenue GetSquashVenueById(int squashVenueId)
        {
            SquashVenue squashVenue = _context.SquashVenues.SingleOrDefault(s => s.SquashVenueId == squashVenueId);
            return squashVenue;
        }

        public void DeleteSquashVenue(int id)
        {
            //need rules here to check that venue is not being used somewhere
            var squashVenue = GetSquashVenueById(id);
            if (squashVenue != null)
            {
                _context.SquashVenues.Remove(squashVenue);
                _context.SaveChanges();
            }
        }

        public void UpdateSquashVenue(SquashVenue squashVenue)
        {
            if (squashVenue.SquashVenueId == 0)
            {
                CreateNewSquashVenue(squashVenue);
            }
            else
            {
                UpdateExistingSquashVenue(squashVenue);
            }
        }

        public void CreateNewSquashVenue(SquashVenue squashVenue)
        {
            _context.Add(squashVenue);
            _context.SaveChanges();
        }

        public void UpdateExistingSquashVenue(SquashVenue squashVenue)
        {
            var _existingSquashVenue = GetSquashVenueById(squashVenue.SquashVenueId);

            _existingSquashVenue.Name = squashVenue.Name;
            _existingSquashVenue.CourtsAvailable = squashVenue.CourtsAvailable;

            _context.SaveChanges();
        }
    }
}
