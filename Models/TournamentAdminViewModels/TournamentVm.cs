using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SquashBotWebCore.Models.SquashBot.Classes.Enums;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class TournamentVm
    {
        public TournamentVm()
        {
            Sections = new List<Section>();
        }

        public TournamentVm(Tournament tournament)
        {
            TournamentId = tournament.TournamentId;
            Name = tournament.Name;
            SectionType = tournament.SectionType;
            StartDate = tournament.StartDate;
            EndDate = tournament.EndDate;
            Sections = new List<Section>();
        }

        public Tournament Tournament()
        {
            Tournament t = new Tournament();
            t.TournamentId = TournamentId;
            t.Name = Name;
            t.SectionType = SectionType;
            t.StartDate = StartDate;
            t.EndDate = EndDate;
            return t;
        }

        public int TournamentId { get; set; }

        public bool IsNew
        {
            get { return TournamentId == 0; }
        }

        [Required]
        [Display(Name = "Tournament")]
        public string Name { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        [Required]
        [Display(Name = "Naming Convention")]
        public SectionType SectionType { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
