using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public class BlackjackTurn : IBlackjackTurn
    {
        public IList<Action<IBlackjackGameFlow>> AvailableActions { get; }

        public Action<IBlackjackGameFlow>? ChosenAction { get; set; }

        public IBlackjackGameFlow Game { get; }

        public IBlackjackPlayer Player { get; }

        IPlayer<IBlackjackCard> ITurn<IBlackjackCard>.Player
            => Player;

        IList<Action<IGameFlow<IBlackjackCard>>> ITurn<IBlackjackCard>.AvailableActions
            => (IList<Action<IGameFlow<IBlackjackCard>>>)AvailableActions;

        Action<IGameFlow<IBlackjackCard>>? ITurn<IBlackjackCard>.ChosenAction
            => ChosenAction as Action<IGameFlow<IBlackjackCard>>;

        IGameFlow<IBlackjackCard> ITurn<IBlackjackCard>.Game
            => Game;

        public BlackjackTurn(
            IBlackjackGameFlow game,
            IBlackjackPlayer player,
            IList<Action<IBlackjackGameFlow>> availableActions)
        {
            Game = game;
            Player = player;
            AvailableActions = availableActions;
        }
    }
}