using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Services
{
    public interface IFixtureManagement
    {
        FixtureIndexVm FixtureIndexViewModel(int sectionId);
        List<Fixture> GetFixturesForSection(int sectionId);
        Fixture GetFixtureById(int fixtureId);
        int DeleteFixture(int id);
        void UpdateFixture(Fixture fixture);
        
    }

    public class FixtureManagement : IFixtureManagement
    {
        private readonly ApplicationDbContext _context;
        private readonly ISectionManagement _sectionManagement;
        private readonly ITeamManagement _teamManagement;
        private readonly ISquashVenueManagement _squashVenueManagement;
        private readonly ITournamentSquashVenueManagement _tournamentSquashVenueManagement;
        private readonly ITournamentManagement _tournamentManagement;

        public FixtureManagement(ApplicationDbContext context,
                                ISectionManagement sectionManagement,
                                ITeamManagement teamManagement,
                                ISquashVenueManagement squashVenueManagement,
                                ITournamentSquashVenueManagement tournamentSquashVenueManagement,
                                ITournamentManagement tournamentManagement)
        {
            _context = context;
            _sectionManagement = sectionManagement;
            _teamManagement = teamManagement;
            _squashVenueManagement = squashVenueManagement;
            _tournamentSquashVenueManagement = tournamentSquashVenueManagement;
            _tournamentManagement = tournamentManagement;
        }

        public FixtureIndexVm FixtureIndexViewModel(int sectionId)
        {
            //clean up here please :-)

            FixtureIndexVm viewModel = new FixtureIndexVm();
            viewModel.SectionVm = new SectionVm(_sectionManagement.GetSectionById(sectionId));
            viewModel.SectionVm.Tournament = _tournamentManagement.GetTournamentById(viewModel.SectionVm.TournamentId);


            foreach (var fixture in GetFixturesForSection(sectionId))
            {
                FixtureVm fixtureVm = new FixtureVm(fixture);
                fixtureVm.TeamA = _teamManagement.GetTeamById(fixture.TeamAId);
                fixtureVm.TeamB = _teamManagement.GetTeamById(fixture.TeamBId);
                fixtureVm.Section = _sectionManagement.GetSectionById(fixture.SectionId);
                fixtureVm.TournamentSquashVenue = _tournamentSquashVenueManagement.GetTournamentSquashVenueById(fixture.TournamentSquashVenueId);                

                viewModel.FixtureVms.Add(fixtureVm);
            }

            return viewModel;
        }

        public List<Fixture> GetFixturesForSection(int sectionId)
        {
            List<Fixture> fixtures = _context.Fixtures
                .Where(f => f.SectionId == sectionId)
                .ToList();

            foreach (var fixture in fixtures)
            {
                //this should be cleaned up :-)
                fixture.TeamA = _teamManagement.GetTeamById(fixture.TeamAId);
                fixture.TeamB = _teamManagement.GetTeamById(fixture.TeamBId);
                fixture.TournamentSquashVenue = _tournamentSquashVenueManagement.GetTournamentSquashVenueById(fixture.TournamentSquashVenueId);
            }

            return fixtures;
        }

        public Fixture GetFixtureById(int fixtureId)
        {
            Fixture fixture = _context.Fixtures.SingleOrDefault(f => f.FixtureId == fixtureId);
            return fixture;
        }

        public int DeleteFixture(int id)
        {
            var fixture = GetFixtureById(id);
            if (fixture != null)
            {
                int sectionId = fixture.SectionId;
                _context.Fixtures.Remove(fixture);
                _context.SaveChanges();

                return sectionId;
            }

            return 0;
        }

        public void UpdateFixture(Fixture fixture)
        {
            if (fixture.FixtureId == 0)
            {
                CreateNewFixture(fixture);
            }
            else
            {
                UpdateExistingFixture(fixture);
            }
        }

        private void CreateNewFixture(Fixture fixture)
        {
            _context.Add(fixture);
            _context.SaveChanges();
        }

        private void UpdateExistingFixture(Fixture fixture)
        {
            var existingFixture = GetFixtureById(fixture.FixtureId);

            existingFixture.TeamAId = fixture.TeamAId;
            existingFixture.TeamBId = fixture.TeamBId;
            existingFixture.SectionId = fixture.SectionId;
            existingFixture.DateTime = fixture.DateTime;
            existingFixture.TournamentSquashVenueId = fixture.TournamentSquashVenueId;
            existingFixture.Court = fixture.Court;

            _context.SaveChanges();
        }
    }
}
