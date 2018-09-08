using System;

namespace SquashBotWebCore.Models.SquashBot.Classes.Interfaces
{
    public interface IModificationHistory
    {
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
    }
}
