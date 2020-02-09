﻿using CardGames.Blackjack.Exceptions;
using CardGames.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackHand : IBlackjackHand
    {
        public const int MAX_CARD_COUNT = 14;

        private readonly IList<IBlackjackCard> _cards;

        public IReadOnlyList<IBlackjackCard> Cards
            => new ReadOnlyCollection<IBlackjackCard>(_cards);

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
    }
}