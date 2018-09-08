using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;

namespace SquashBotWebCore.Models.TournamentAdminViewModels
{
    public class SquashVenueVm
    {
        public SquashVenueVm()
        {
            
        }

        public SquashVenueVm(SquashVenue squashVenue)
        {
            SquashVenueId = squashVenue.SquashVenueId;
            Name = squashVenue.Name;
            CourtsAvailable = squashVenue.CourtsAvailable;
        }

        public SquashVenue SquashVenue()
        {
            SquashVenue sv = new SquashVenue();
            sv.SquashVenueId = SquashVenueId;
            sv.Name = Name;
            sv.CourtsAvailable = CourtsAvailable;

            return sv;
        }

        public bool IsNew => SquashVenueId == 0;

        public int SquashVenueId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Courts Available")]
        public int CourtsAvailable { get; set; }
    }
}
