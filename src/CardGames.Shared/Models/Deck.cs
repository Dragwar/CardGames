using CardGames.Shared.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CardGames.Shared.Models
{
    public class Deck : IDeck<ICard>
    {
        public static readonly int _totalCardCount = SortedCards.Count;

        public static readonly int _suitCount = Enum
            .GetValues(typeof(Suit))
            .Cast<Suit>()
            .Count();

        public static IReadOnlyList<ICard> SortedCards
            => new ReadOnlyCollection<ICard>(Enum
            .GetValues(typeof(Suit))
            .Cast<Suit>()
            .SelectMany(suit =>
                Enum
                .GetValues(typeof(CardNameValue))
                .Cast<CardNameValue>()
                .Select(cardValue => new Card(cardValue, suit)))
            .ToArray());

        private readonly IShuffleService<ICard> _shuffleService;
        private readonly IList<ICard> _cards;

        public IReadOnlyList<ICard> Cards
            => new ReadOnlyCollection<ICard>(_cards);

        /// <inheritdoc cref="FillDeck(bool)" />
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        public Deck(IShuffleService<ICard> shuffleService)
        {
            _shuffleService = shuffleService;
            _cards = new List<ICard>(_totalCardCount);
        }

        /// <inheritdoc cref="FillDeck(bool)"/>
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        /// <param name="initialCards">Cards to be inserted into the deck.</param>
        public Deck(IShuffleService<ICard> shuffleService, IEnumerable<ICard> initialCards, bool isShuffled)
        {
            _shuffleService = shuffleService;
            _cards = new List<ICard>(initialCards);

            if (_cards.Count > _totalCardCount)
                throw new InvalidOperationException($"surpassed max card count {_totalCardCount}. actual count: {_cards.Count}");

            if (isShuffled)
                Shuffle();
        }

        /// <inheritdoc cref="FillDeck(bool)" />
        /// <param name="shuffleService">A service that is required for shuffling the deck.</param>
        public Deck(IShuffleService<ICard> shuffleService, bool isShuffled)
            : this(shuffleService: shuffleService)
            => FillDeck(isShuffled);

        public void Shuffle()
        {
            var tempList = new List<ICard>(_cards);
            _cards.Clear();
            if (_cards is List<ICard> list)
            {
                list.AddRange(_shuffleService.Shuffle(tempList));
            }
            else
            {
                foreach (var card in _shuffleService.Shuffle(tempList))
                    _cards.Add(card);
            }
        }

        public void FillDeck(bool isShuffled = true)
        {
            _cards.Clear();
            if (_cards is List<ICard> list)
            {
                list.AddRange(isShuffled ? _shuffleService.Shuffle(SortedCards) : SortedCards);
            }
            else
            {
                foreach (var card in isShuffled ? _shuffleService.Shuffle(SortedCards) : SortedCards)
                    _cards.Add(card);
            }
        }

        [return: MaybeNull]
        public ICard? Peek([AllowNull] int? cardIndex = null)
            => cardIndex is { }
            ? _cards.ElementAtOrDefault(cardIndex.Value)
            : _cards.ElementAtOrDefault(0);

        [return: MaybeNull]
        public ICard? DealOrDefault([AllowNull] int? cardIndex = null)
            => Peek(cardIndex) is { } card && _cards.Remove(card)
            ? card
            : null;

        [return: NotNull]
        public ICard Deal([AllowNull] int? cardIndex = null)
            => DealOrDefault(cardIndex)
            ?? throw new NullReferenceException();

        [return: NotNull]
        public IEnumerable<ICard> DealAll()
        {
            int count = _cards.Count;
            for (int i = 0; i < count; i++)
                yield return Deal();
        }

        public IEnumerator<ICard> GetEnumerator()
            => Cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}