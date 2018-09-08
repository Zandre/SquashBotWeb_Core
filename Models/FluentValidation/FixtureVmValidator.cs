using System;
using FluentValidation;
using SquashBotWebCore.Models.SquashBot.Classes.TournamentClasses;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class FixtureVmValidator : AbstractValidator<FixtureVm>
    {
        public FixtureVmValidator(Tournament tournament, TournamentSquashVenue tournamentSquashVenue)
        {
            RuleFor(x => x.TeamAId)
                .NotEqual(0)
                .WithMessage("Please select Team A");

            RuleFor(x => x.TeamBId)
                .NotEqual(0)
                .WithMessage("Please select Team B");

            RuleFor(x => x.TeamBId)
                .NotEqual(x => x.TeamAId)
                .WithMessage("Please select different teams");


            RuleFor(x => x.DateTime)
                .GreaterThanOrEqualTo(tournament.StartDate)
                .WithMessage("Fixture Date must be later than Tournament Start Date (" + tournament.StartDate.ToShortDateString() + ")");

            RuleFor(x => x.DateTime)
                .LessThanOrEqualTo(tournament.EndDate)
                .WithMessage("Fixture Date must be before Tournament End Date (" + tournament.EndDate.ToShortDateString() + ")");

            RuleFor(x => x.TournamentSquashVenueId)
                .NotEmpty()
                .WithMessage("Venue cannot be empty");

            RuleFor(x => x.Court)
                .NotEmpty()
                .WithMessage("Court cannot be empty");

            //RuleFor(x => Convert.ToInt32(x.Court))
            //    .LessThanOrEqualTo(tournamentSquashVenue.SquashVenue.CourtsAvailable)
            //    .WithMessage(tournamentSquashVenue.SquashVenue.Name
            //                 + " only has "
            //                 + tournamentSquashVenue.SquashVenue.CourtsAvailable
            //                 + " courts available");

            RuleFor(x => x.Court)
                .Must(y => Convert.ToInt32(y) <= tournamentSquashVenue.SquashVenue.CourtsAvailable)
                .WithMessage(tournamentSquashVenue.SquashVenue.Name
                             + " only has "
                             + tournamentSquashVenue.SquashVenue.CourtsAvailable
                             + " courts available");
        }
    }
}
