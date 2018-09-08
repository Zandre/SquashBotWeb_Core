using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class SquashVenueVmValidator : AbstractValidator<SquashVenueVm>
    {
        public SquashVenueVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Venue Name cannot be empty");

            RuleFor(x => x.CourtsAvailable)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Courts Available must have a positive value");
        }
    }
}
