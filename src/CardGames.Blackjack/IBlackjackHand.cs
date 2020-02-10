using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public interface IBlackjackHand
    {
        IReadOnlyList<IBlackjackCard> Cards { get; }
        IBlackjackCard? HighCard { get; }
        bool IsBlackjack { get; }
        bool IsBust { get; }
        int TotalValue { get; }

        void Add(IBlackjackCard card);

        void Add(ICard card);

        void AddRange(IEnumerable<IBlackjackCard> cards);

        void AddRange(IEnumerable<ICard> cards);

        IEnumerator<IBlackjackCard> GetEnumerator();

        bool Remove(IBlackjackCard card);

        bool Remove(ICard card);

        int RemoveAll(Func<IBlackjackCard, bool> match);

        int RemoveAll(Func<ICard, bool> match);

        void RemoveRange(int index, int count);

        void Sort();
    }
}