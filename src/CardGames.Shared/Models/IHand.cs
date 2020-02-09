using System;
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
        /// Cards belonging to this hand.
        /// </summary>
        IReadOnlyList<TCard> Cards { get; }

        /// <summary>
        /// Sorts by <see cref="ICard.Value"/> ascending.
        /// </summary>
        void Sort();

        /// <inheritdoc cref="List{T}.Add(T)" />
        /// <param name="card"><inheritdoc cref="List{T}.Add(T)" /></param>
        void Add(TCard card);

        /// <inheritdoc cref="List{T}.AddRange(IEnumerable{T})" />
        /// <param name="cards"><inheritdoc cref="List{T}.AddRange(IEnumerable{T})" /></param>
        void AddRange(IEnumerable<TCard> cards);

        /// <inheritdoc cref="List{T}.Remove(T)" />
        bool Remove(TCard card);

        /// <inheritdoc cref="List{T}.RemoveRange(int, int)" />
        void RemoveRange(int index, int count);

        /// <inheritdoc cref="List{T}.RemoveAll(Predicate{T})" />
        int RemoveAll(Func<TCard, bool> match);
    }
}