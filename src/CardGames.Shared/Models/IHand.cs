using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a collection of <see cref="ICard"/>s belonging to an <see cref="Owner"/>.
    /// </summary>
    public interface IHand : IEnumerable<ICard>
    {
        /// <summary>
        /// Cards belonging to this hand.
        /// </summary>
        IReadOnlyList<ICard> Cards { get; }

        /// <summary>
        /// The Card with the highest value within this hand. (based on <see cref="ICard.Value"/>)
        /// </summary>
        ICard? HighCard { get; }

        /// <summary>
        /// The sum of all the cards in this hand. (based on <see cref="ICard.Value"/>)
        /// </summary>
        int TotalValue { get; }

        /// <summary>
        /// Sorts by <see cref="ICard.Value"/> ascending.
        /// </summary>
        void Sort();

        /// <inheritdoc cref="List{T}.Add(T)" />
        /// <param name="card"><inheritdoc cref="List{T}.Add(T)" /></param>
        void Add(ICard card);

        /// <inheritdoc cref="List{T}.AddRange(IEnumerable{T})" />
        /// <param name="cards"><inheritdoc cref="List{T}.AddRange(IEnumerable{T})" /></param>
        void AddRange(IEnumerable<ICard> cards);

        /// <inheritdoc cref="List{T}.Remove(T)" />
        bool Remove(ICard card);

        /// <inheritdoc cref="List{T}.RemoveRange(int, int)" />
        void RemoveRange(int index, int count);

        /// <inheritdoc cref="List{T}.RemoveAll(Predicate{T})" />
        int RemoveAll(Func<ICard, bool> match);
    }
}