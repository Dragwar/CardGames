using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents <see cref="Player"/>'s action during a <see cref="Game"/>.
    /// </summary>
    public interface ITurn
    {
        /// <summary>
        /// Represents the actions that the <see cref="Player"/> can take during this turn.
        /// </summary>
        IList<ITurnAction> AvailableActions { get; }

        /// <summary>
        /// The action perform during this turn.
        /// </summary>
        ITurnAction? ChosenAction { get; }

        /// <summary>
        /// The game that this turn belongs to.
        /// </summary>
        IGameFlow Game { get; }

        /// <summary>
        /// The owner of this turn.
        /// </summary>
        IPlayer Player { get; }
    }
}