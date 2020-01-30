using CardGames.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a collection of 52 (<see cref="_totalCardCount"/>) <see cref="Card"/>s.
    /// </summary>
    public class Deck
    {
        public static readonly int _totalCardCount = SortedCards.Count;

        public static readonly int _suitCount = Enum
            .GetValues(typeof(Suit))
            .Cast<Suit>()
            .Count();

        public static IReadOnlyList<Card> SortedCards
            => new ReadOnlyCollection<Card>(Enum
            .GetValues(typeof(Suit))
            .Cast<Suit>()
            .SelectMany(suit =>
                Enum
                .GetValues(typeof(CardNameValue))
                .Cast<CardNameValue>()
                .Select(cardValue => new Card(cardValue, suit)))
            .ToArray());

        private readonly IShuffleService<Card> _shuffleService;
        private readonly IList<Card> _cards;

        public IReadOnlyList<Card> Cards
            => new ReadOnlyCollection<Card>(_cards);

        /// <inheritdoc cref="FillDeck(bool)" />
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        public Deck(IShuffleService<Card> shuffleService)
        {
            _shuffleService = shuffleService;
            _cards = new List<Card>(_totalCardCount);
        }

        /// <inheritdoc cref="FillDeck(bool)"/>
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        /// <param name="initialCards">Cards to be inserted into the deck.</param>
        public Deck(IShuffleService<Card> shuffleService, IEnumerable<Card> initialCards, bool isShuffled)
        {
            _shuffleService = shuffleService;
            _cards = new List<Card>(initialCards);

            if (_cards.Count > _totalCardCount)
                throw new InvalidOperationException($"surpassed max card count {_totalCardCount}. actual count: {_cards.Count}");

            if (isShuffled)
                Shuffle();
        }

        /// <inheritdoc cref="FillDeck(bool)" />
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        public Deck(IShuffleService<Card> shuffleService, bool isShuffled)
            : this(shuffleService: shuffleService)
            => FillDeck(isShuffled);

        /// <summary>
        /// Shuffles the current <see cref="Cards"/> and retains the same <see cref="Card"/>s.
        /// </summary>
        public void Shuffle()
        {
            var tempList = new List<Card>(_cards);
            _cards.Clear();
            if (_cards is List<Card> list)
            {
                list.AddRange(_shuffleService.Shuffle(tempList));
            }
            else
            {
                foreach (var card in _shuffleService.Shuffle(tempList))
                    _cards.Add(card);
            }
        }

        /// <summary>
        /// Builds/Rebuilds the deck with 52 <see cref="Card"/>s.
        /// </summary>
        /// <param name="isShuffled">Determines whether to shuffle the deck or leave it sorted.</param>
        public void FillDeck(bool isShuffled = true)
        {
            _cards.Clear();
            if (_cards is List<Card> list)
            {
                list.AddRange(isShuffled ? _shuffleService.Shuffle(SortedCards) : SortedCards);
            }
            else
            {
                foreach (var card in isShuffled ? _shuffleService.Shuffle(SortedCards) : SortedCards)
                    _cards.Add(card);
            }
        }

        /// <summary>
        /// Gets a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">The index at which the card you wish to peek resides.</param>
        [return: MaybeNull]
        public Card? Peek([AllowNull] int? cardIndex = null)
            => cardIndex is { }
            ? _cards.ElementAtOrDefault(cardIndex.Value)
            : _cards.ElementAtOrDefault(0);

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// Will return <see langword="null"/> when deck is empty or no card is at <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        [return: MaybeNull]
        public Card? DealOrDefault([AllowNull] int? cardIndex = null)
            => Peek(cardIndex) is { } card && _cards.Remove(card)
            ? card
            : null;

        /// <summary>
        /// Removes a card from the top of the deck or at the <paramref name="cardIndex"/>.
        /// </summary>
        /// <param name="cardIndex">(Optional) The index at which the card you wish to deal resides.</param>
        /// <exception cref="NullReferenceException">throws when the deck is empty or no card is at <paramref name="cardIndex"/>.</exception>
        [return: NotNull]
        public Card Deal([AllowNull] int? cardIndex = null)
            => DealOrDefault(cardIndex)
            ?? throw new NullReferenceException();

        /// <summary>
        /// Removes all remaining cards from deck.
        /// </summary>
        [return: NotNull]
        public IEnumerable<Card> DealAll()
        {
            for (int i = 0; i < _cards.Count; i++)
                yield return _cards[i];
            _cards.Clear();
        }
    }
}