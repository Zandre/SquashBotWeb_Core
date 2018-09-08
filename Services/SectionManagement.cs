using System.Collections.Generic;
using System.Linq;
using SquashBotWebCore.Data;
using SquashBotWebCore.Models.SquashBot.Classes.Enums;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentViewerViewModel;

namespace SquashBotWebCore.Services
{
    public interface ISectionManagement
    {
        Section GetSectionById(int sectionId);
        List<Section> GetSectionsForTournament(int tournamentId);
        void UpdateSection(Section section);
        int DeleteSection(int id);

        List<SectionDetailsVm> GetMenSectionsForTournament(int tournamentId);
        List<SectionDetailsVm> GetWomenSectionsForTournament(int tournamentId);
        List<SectionDetailsVm> GetMixedSectionsForTournament(int tournamentId);
    }

    public class SectionManagement : ISectionManagement
    {
        private readonly ApplicationDbContext _context;

        public SectionManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public Section GetSectionById(int sectionId)
        {
            Section section = _context.Section.SingleOrDefault(s => s.SectionId == sectionId);
            return section;
        }

        public List<Section> GetSectionsForTournament(int tournamentId)
        {
            List<Section> sections = _context.Section
                                                .Where(s => s.TournamentId == tournamentId)
                                                .OrderBy(x => x.Gender)
                                                .ThenBy(x => x.Name)
                                                .ToList();
            return sections;
        }

        public List<SectionDetailsVm> GetMenSectionsForTournament(int tournamentId)
        {
            List<Section> sections = _context.Section
                                            .Where(s => s.TournamentId == tournamentId &&
                                                        s.Gender == SectionGender.Men)
                                            .OrderBy(x => x.Name)
                                            .ToList();

            List<SectionDetailsVm> sectionDetailsVms = new List<SectionDetailsVm>();
            foreach (var section in sections)
            {
                SectionDetailsVm sectionDetailsVm = new SectionDetailsVm(section);
                sectionDetailsVms.Add(sectionDetailsVm);
            }
            return sectionDetailsVms;
        }

        public List<SectionDetailsVm> GetWomenSectionsForTournament(int tournamentId)
        {
            List<Section> sections = _context.Section
                                            .Where(s => s.TournamentId == tournamentId &&
                                                        s.Gender == SectionGender.Women)
                                            .OrderBy(x => x.Name)
                                            .ToList();

            List<SectionDetailsVm> sectionDetailsVms = new List<SectionDetailsVm>();
            foreach (var section in sections)
            {
                SectionDetailsVm sectionDetailsVm = new SectionDetailsVm(section);
                sectionDetailsVms.Add(sectionDetailsVm);
            }
            return sectionDetailsVms;
        }

        public List<SectionDetailsVm> GetMixedSectionsForTournament(int tournamentId)
        {
            List<Section> sections = _context.Section
                                            .Where(s => s.TournamentId == tournamentId &&
                                                        s.Gender == SectionGender.Women)
                                            .OrderBy(x => x.Name)
                                            .ToList();

            List<SectionDetailsVm> sectionDetailsVms = new List<SectionDetailsVm>();
            foreach (var section in sections)
            {
                SectionDetailsVm sectionDetailsVm = new SectionDetailsVm(section);
                sectionDetailsVms.Add(sectionDetailsVm);
            }
            return sectionDetailsVms;
        }

        public void UpdateSection(Section section)
        {
            if (section.SectionId == 0)
            {
                CreateNewSection(section);
            }
            else
            {
                SaveExistingSection(section);
            }
        }

        private void CreateNewSection(Section section)
        {
            _context.Add(section);
            _context.SaveChanges();
        }

        private void SaveExistingSection(Section section)
        {
            var _section = GetSectionById(section.SectionId);

            _section.Name = section.Name;
            _section.PlayersPerTeam = section.PlayersPerTeam;
            _section.Gender = section.Gender;
            _section.Par = section.Par;

            _context.SaveChanges();
        }

        public int DeleteSection(int id)
        {
            var section = GetSectionById(id);
            if (section != null)
            {
                int tournamentId = section.TournamentId;
                _context.Section.Remove(section);
                _context.SaveChanges();

                return tournamentId;
            }

            return 0;
        }
    }
}
