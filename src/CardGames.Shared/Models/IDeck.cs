using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a collection of 52 (<see cref="Deck._totalCardCount"/>) <ses cref="ICard"/>s.
    /// </summary>
    public interface IDeck : IEnumerable<ICard>
    {
        /// <summary>
        /// Cards belonging to this deck.
        /// </summary>
        IReadOnlyList<ICard> Cards { get; }

        bool IsEmpty { get; }

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        /// <exception cref="NullReferenceException">throws when the deck is empty or no card is at <paramref name="cardIndex"/>.</exception>
        ICard Deal([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Removes all remaining cards from deck.
        /// </summary>
        IEnumerable<ICard> DealAll();

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        ICard? DealOrDefault([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Builds/Rebuilds the deck with 52 <typeparamref name="ICard"/>s.
        /// </summary>
        /// <param name="isShuffled">Determines whether to shuffle the deck or leave it sorted.</param>
        void FillDeck(bool isShuffled = true);

        /// <summary>
        /// Gets a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">The index at which the card you wish to peek resides.</param>
        ICard? Peek([AllowNull] int? cardIndex = null);

        /// <summary>
        /// Shuffles the current <see cref="Cards"/> and retains the same <typeparamref name="ICard"/>s.
        /// </summary>
        void Shuffle();
    }
}