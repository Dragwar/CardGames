using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public interface IBlackjackHand : IHand, IEnumerable<IBlackjackCard>
    {
        //IReadOnlyList<IBlackjackCard> Cards { get; }
        //IBlackjackCard? HighCard { get; }
        bool IsBlackjack { get; }
        bool IsBust { get; }
        //int TotalValue { get; }

        void Add(IBlackjackCard card);

        //void Add(ICard card);

        void AddRange(IEnumerable<IBlackjackCard> cards);

        //void AddRange(IEnumerable<ICard> cards);

        bool Remove(IBlackjackCard card);

        //bool Remove(ICard card);

        int RemoveAll(Func<IBlackjackCard, bool> match);

        //int RemoveAll(Func<ICard, bool> match);

        //void RemoveRange(int index, int count);

        //void Sort();
    }
}