using FluentValidation;
using SquashBotWebCore.Models.TournamentAdminViewModels;

namespace SquashBotWebCore.Models.FluentValidation
{
    public class RemoveTournamentAdministratorVmValidator : AbstractValidator<TournamentAdministratorVm>
    {
        public RemoveTournamentAdministratorVmValidator()
        {
            RuleFor(x => x.AdminUsers.Count)
                .GreaterThan(1)
                .WithMessage("There must always be at least 1 Tournament Administrator remaining");
        }
    }
}
