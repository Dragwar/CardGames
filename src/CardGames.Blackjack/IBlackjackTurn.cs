using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public interface IBlackjackTurn
    {
        IList<Action<IBlackjackGameFlow>> AvailableActions { get; }
        Action<IBlackjackGameFlow>? ChosenAction { get; set; }
        IBlackjackGameFlow Game { get; }
        IBlackjackPlayer Player { get; }
    }
}