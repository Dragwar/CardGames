using CardGames.Blackjack.Exceptions;
using CardGames.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackHand : IHand, IBlackjackHand
    {
        public const int MAX_CARD_COUNT = 14;

        private readonly IList<IBlackjackCard> _cards;

        public IReadOnlyList<IBlackjackCard> Cards
            => new ReadOnlyCollection<IBlackjackCard>(_cards);

        public IBlackjackCard? HighCard
            => Cards
            .OrderByDescending(c => c.Value)
            .FirstOrDefault();

        public int TotalValue
            => Cards.Sum(c => c.Value);

        public bool IsBust
            => TotalValue >= 22;

        public bool IsBlackjack
            => TotalValue % 21 == 0;

        IReadOnlyList<ICard> IHand.Cards
            => Cards
            .Cast<ICard>()
            .ToList();

        ICard? IHand.HighCard
            => HighCard as ICard;

        public BlackjackHand(IList<IBlackjackCard> cards)
        {
            _cards = cards;

            if (cards.Count > MAX_CARD_COUNT)
            {
                throw new BlackjackHandSizeTooLargeException(this);
            }
        }

        public BlackjackHand()
        {
            _cards = new List<IBlackjackCard>(MAX_CARD_COUNT) { Capacity = MAX_CARD_COUNT };
        }

        public void Sort()
        {
            var tempList = new List<IBlackjackCard>(_cards.OrderBy(card => card.Value));
            _cards.Clear();
            tempList.ForEach(_cards.Add);
        }

        public void Add(IBlackjackCard card)
        {
            _cards.Add(card);

            if (_cards.Count > MAX_CARD_COUNT)
            {
                throw new BlackjackHandSizeTooLargeException(this);
            }
        }

        public void AddRange(IEnumerable<IBlackjackCard> cards)
        {
            if (_cards is List<IBlackjackCard> cardList)
            {
                cardList.AddRange(cards);
            }
            else
            {
                var temp = new List<IBlackjackCard>(cards);
                temp.ForEach(_cards.Add);
            }

            if (_cards.Count > MAX_CARD_COUNT)
            {
                throw new BlackjackHandSizeTooLargeException(this);
            }
        }

        public bool Remove(IBlackjackCard card)
            => _cards.Remove(card);

        public void RemoveRange(int index, int count)
        {
            if (_cards.Count < count)
            {
                throw new ArgumentException(nameof(count));
            }

            if (_cards.ElementAtOrDefault(index) is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (_cards is List<IBlackjackCard> cards)
            {
                cards.RemoveRange(index, count);
                return;
            }

            for (int i = index; i < count; i++)
            {
                _cards.RemoveAt(i);
            }
        }

        public int RemoveAll(Func<IBlackjackCard, bool> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (_cards is List<IBlackjackCard> cards)
            {
                return cards.RemoveAll(new Predicate<IBlackjackCard>(match));
            }

            var removeCount = 0;
            for (int i = 0; i < _cards.Count; i++)
            {
                if (match(_cards[i]))
                {
                    _cards.Remove(_cards[i]);
                    removeCount++;
                }
            }

            return removeCount;
        }

        public void Add(ICard card)
            => Add((IBlackjackCard)card);

        public void AddRange(IEnumerable<ICard> cards)
            => AddRange(cards.Cast<IBlackjackCard>());

        public bool Remove(ICard card)
            => Remove((IBlackjackCard)card);

        public int RemoveAll(Func<ICard, bool> match)
            => RemoveAll((Func<IBlackjackCard, bool>)match);

        public IEnumerator<IBlackjackCard> GetEnumerator()
            => Cards.GetEnumerator();

        IEnumerator<ICard> IEnumerable<ICard>.GetEnumerator()
            => Cards
            .Cast<ICard>()
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}