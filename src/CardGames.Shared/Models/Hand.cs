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

        public IPlayer<ICard> Owner { get; }

        public Hand(IPlayer<ICard> owner, IList<ICard> cards)
        {
            _cards = cards;
            Owner = owner;
        }

        public Hand(IPlayer<ICard> owner)
        {
            _cards = new List<ICard>(26);
            Owner = owner;
        }

        public void Sort()
        {
            var tempList = new List<ICard>(_cards.OrderBy(card => card.Value));
            _cards.Clear();
            tempList.ForEach(_cards.Add);
        }
    }
}