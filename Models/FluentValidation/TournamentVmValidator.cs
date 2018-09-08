using System;
using FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class TournamentVmValidator : AbstractValidator<TournamentVm>
    {
        public TournamentVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tournament Name cannot be empty");

            RuleFor(x => x.SectionType)
                .NotEmpty()
                 .WithMessage("Naming convention cannot be empty");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start Date cannot be backdated");

            RuleFor(x => x.EndDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("End Date cannot be backdated");

            RuleFor(x => x.EndDate)
                .GreaterThan(y => y.StartDate)
                .WithMessage("End Date cannot be before Start Date");

        }
    }
}