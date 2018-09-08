using FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class TeamVmValidator : AbstractValidator<TeamVm>
    {
        public TeamVmValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team Name cannot be empty");
        }
    }
}
