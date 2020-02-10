using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public interface IBlackjackGameFlow : IGameFlow
    {
        //IBlackjackTurn? CurrentTurn { get; set; }
        //IBlackjackDeck Deck { get; }
        //string Name { get; }
        //IReadOnlyList<IBlackjackPlayer> Players { get; }
        //IList<IBlackjackTurn> TurnHistory { get; }

        //void Deal(IBlackjackPlayer player);
        //void Deal(IPlayer player);
        //void SetTurnOrder<TKey>(Func<IBlackjackPlayer, TKey> reorderFunction);
        //void SetTurnOrder<TKey>(Func<IPlayer, TKey> reorderFunction);
    }
}