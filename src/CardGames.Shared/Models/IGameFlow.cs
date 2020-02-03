using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents the flow of a card game.
    /// </summary>
    /// <typeparam name="TCard">The type of card being used.</typeparam>
    public interface IGameFlow<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        /// The current turn of this game.
        /// </summary>
        ITurn<TCard>? CurrentTurn { get; }

        /// <summary>
        /// The deck associated with this game.
        /// </summary>
        IDeck<TCard> Deck { get; }

        /// <summary>
        /// Name of the game.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The players participating in this game.
        /// </summary>
        IReadOnlyList<IPlayer<TCard>> Players { get; }

        /// <summary>
        /// History of all turns taken during this game.
        /// </summary>
        IList<ITurn<TCard>> TurnHistory { get; }

        void EndGame(IPlayer<TCard> winner);

        void SetTurnOrder<TKey>(Func<IPlayer<TCard>, TKey> reorderFunction);

        void StartGame();
    }
}