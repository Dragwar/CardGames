using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents the flow of a card game.
    /// </summary>
    public interface IGameFlow
    {
        /// <summary>
        /// The current turn of this game.
        /// </summary>
        ITurn? CurrentTurn { get; }

        /// <summary>
        /// The deck associated with this game.
        /// </summary>
        IDeck Deck { get; }

        /// <summary>
        /// Name of the game.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The players participating in this game.
        /// </summary>
        IReadOnlyList<IPlayer> Players { get; }

        /// <summary>
        /// History of all turns taken during this game.
        /// </summary>
        IList<ITurn> TurnHistory { get; }

        IList<ITurn> TurnOrder { get; }

        void SetTurnOrder<TKey>(Func<IPlayer, TKey> reorderFunction);

        void Deal(IPlayer player);
    }
}