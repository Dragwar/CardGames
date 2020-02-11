using CardGames.Shared;
using CardGames.Shared.Models;
using CardGames.Shared.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackDeck : IDeck, IBlackjackDeck
    {
        public static readonly int _totalCardCount = Deck._totalCardCount;
        public static readonly int _suitCount = Deck._suitCount;

        public static IReadOnlyList<IBlackjackCard> SortedCards
            => Deck.SortedCards as IReadOnlyList<IBlackjackCard>
            ?? new ReadOnlyCollection<IBlackjackCard>(Enum
                .GetValues(typeof(Suit))
                .Cast<Suit>()
                .SelectMany(suit =>
                    Enum
                    .GetValues(typeof(CardNameValue))
                    .Cast<CardNameValue>()
                    .Select(cardValue => new BlackjackCard(cardValue, suit)))
                .ToArray());

        private readonly IShuffleService<IBlackjackCard> _shuffleService;
        private readonly IList<IBlackjackCard> _cards;

        public IReadOnlyList<IBlackjackCard> Cards
            => new ReadOnlyCollection<IBlackjackCard>(_cards);

        IReadOnlyList<ICard> IDeck.Cards
            => Cards
            .Cast<ICard>()
            .ToList();
        public bool IsEmpty
            => Cards.Any();

        public BlackjackDeck(IShuffleService<IBlackjackCard> shuffleService)
        {
            _shuffleService = shuffleService;
            _cards = new List<IBlackjackCard>(_totalCardCount);
        }

        public BlackjackDeck(
            IShuffleService<IBlackjackCard> shuffleService,
            IEnumerable<IBlackjackCard> initialCards,
            bool isShuffled)
        {
            _shuffleService = shuffleService;
            _cards = new List<IBlackjackCard>(initialCards);

            if (_cards.Count > _totalCardCount)
                throw new InvalidOperationException($"surpassed max card count {_totalCardCount}. actual count: {_cards.Count}");

            if (isShuffled)
                Shuffle();
        }

        public BlackjackDeck(IShuffleService<IBlackjackCard> shuffleService, bool isShuffled)
            : this(shuffleService: shuffleService)
            => FillDeck(isShuffled);

        public void Shuffle()
        {
            var tempList = new List<IBlackjackCard>(_cards);
            _cards.Clear();
            if (_cards is List<IBlackjackCard> list)
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
            if (_cards is List<IBlackjackCard> list)
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
        public IBlackjackCard? Peek([AllowNull] int? cardIndex = null)
            => cardIndex is { }
            ? _cards.ElementAtOrDefault(cardIndex.Value)
            : _cards.ElementAtOrDefault(0);

        [return: MaybeNull]
        public IBlackjackCard? DealOrDefault([AllowNull] int? cardIndex = null)
            => Peek(cardIndex) is { } card && _cards.Remove(card)
            ? card
            : null;

        [return: NotNull]
        public IBlackjackCard Deal([AllowNull] int? cardIndex = null)
            => DealOrDefault(cardIndex)
            ?? throw new NullReferenceException();

        [return: NotNull]
        public IEnumerable<IBlackjackCard> DealAll()
        {
            int count = _cards.Count;
            for (int i = 0; i < count; i++)
                yield return Deal();
        }

        ICard IDeck.Deal(int? cardIndex)
            => (ICard)Deal(cardIndex);

        IEnumerable<ICard> IDeck.DealAll()
            => (IEnumerable<ICard>)DealAll();

        ICard? IDeck.DealOrDefault(int? cardIndex)
            => DealOrDefault(cardIndex) as ICard;

        ICard? IDeck.Peek(int? cardIndex)
            => Peek(cardIndex) as ICard;

        public IEnumerator<IBlackjackCard> GetEnumerator()
            => Cards.GetEnumerator();

        IEnumerator<ICard> IEnumerable<ICard>.GetEnumerator()
            => Cards.Cast<ICard>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}