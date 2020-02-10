using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Shared.Models
{
    public class Hand : IHand<ICard>
    {
        private readonly IList<ICard> _cards;

        public IReadOnlyList<ICard> Cards
            => new ReadOnlyCollection<ICard>(_cards);

        public ICard? HighCard
            => Cards.OrderByDescending(c => c.Value)
            .FirstOrDefault();

        public int TotalValue
            => Cards.Sum(c => c.Value);

        public Hand(IList<ICard> cards)
        {
            _cards = cards;
        }

        public Hand()
        {
            _cards = new List<ICard>(26);
        }

        public void Sort()
        {
            var tempList = new List<ICard>(_cards.OrderBy(card => card.Value));
            _cards.Clear();
            tempList.ForEach(_cards.Add);
        }

        public void Add(ICard card)
            => _cards.Add(card);

        public void AddRange(IEnumerable<ICard> cards)
        {
            if (_cards is List<ICard> cardList)
            {
                cardList.AddRange(cards);
                return;
            }

            var temp = new List<ICard>(cards);
            temp.ForEach(_cards.Add);
        }

        public bool Remove(ICard card)
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

            if (_cards is List<ICard> cards)
            {
                cards.RemoveRange(index, count);
                return;
            }

            for (int i = index; i < count; i++)
            {
                _cards.RemoveAt(i);
            }
        }

        public int RemoveAll(Func<ICard, bool> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (_cards is List<ICard> cards)
            {
                return cards.RemoveAll(new Predicate<ICard>(match));
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

        public IEnumerator<ICard> GetEnumerator()
            => Cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}