using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a collection of 52 (<see cref="Deck._totalCardCount"/>) <typeparamref name="TCard"/>s.
    /// </summary>
    public interface IDeck<TCard> : IEnumerable<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        /// Cards belonging to this deck.
        /// </summary>
        IReadOnlyList<TCard> Cards { get; }

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        /// <exception cref="NullReferenceException">throws when the deck is empty or no card is at <paramref name="cardIndex"/>.</exception>
        TCard Deal([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Removes all remaining cards from deck.
        /// </summary>
        IEnumerable<TCard> DealAll();

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        TCard? DealOrDefault([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Builds/Rebuilds the deck with 52 <typeparamref name="TCard"/>s.
        /// </summary>
        /// <param name="isShuffled">Determines whether to shuffle the deck or leave it sorted.</param>
        void FillDeck(bool isShuffled = true);

        /// <summary>
        /// Gets a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">The index at which the card you wish to peek resides.</param>
        TCard? Peek([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Shuffles the current <see cref="Cards"/> and retains the same <typeparamref name="TCard"/>s.
        /// </summary>
        void Shuffle();
    }
}