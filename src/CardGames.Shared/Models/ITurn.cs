using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents <see cref="Player"/>'s action during a <see cref="Game"/>.
    /// </summary>
    /// <typeparam name="TCard">Represents the type of card.</typeparam>
    public interface ITurn<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        /// Represents the actions that the <see cref="Player"/> can take during this turn.
        /// </summary>
        IList<Action<IGameFlow<TCard>>> AvailableActions { get; }

        /// <summary>
        /// The action perform during this turn.
        /// </summary>
        Action<IGameFlow<TCard>>? ChosenAction { get; }

        /// <summary>
        /// The game that this turn belongs to.
        /// </summary>
        IGameFlow<TCard> Game { get; }

        /// <summary>
        /// The owner of this turn.
        /// </summary>
        IPlayer<TCard> Player { get; }
    }
}