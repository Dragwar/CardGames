using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a collection of <typeparamref name="TCard"/>s belonging to an <see cref="Owner"/>.
    /// </summary>
    public interface IHand<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        /// Owner of this hand.
        /// </summary>
        IPlayer<TCard> Owner { get; }

        /// <summary>
        /// Cards belonging to this hand.
        /// </summary>
        IReadOnlyList<TCard> Cards { get; }

        /// <summary>
        /// Sorts by <see cref="ICard.Value"/> ascending.
        /// </summary>
        void Sort();
    }
}