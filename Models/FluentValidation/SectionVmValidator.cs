using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class SectionVmValidator : AbstractValidator<SectionVm>
    {
        public SectionVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Section Name cannot be empty");

            RuleFor(x => x.PlayersPerTeam)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Players per team must have a positive value");

            RuleFor(x => x.Par)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Game Par must have a positive value");

            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("Section Gender must be specified");
        }
    }
}
