using CardGames.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackTurn : ITurn, IBlackjackTurn
    {
        public IList<Action<IBlackjackGameFlow>> AvailableActions { get; }

        public Action<IBlackjackGameFlow>? ChosenAction { get; set; }

        public IBlackjackGameFlow Game { get; }

        public IBlackjackPlayer Player { get; }

        IPlayer ITurn.Player
            => (IPlayer)Player;

        IList<Action<IGameFlow>> ITurn.AvailableActions
            => AvailableActions
            .Cast<Action<IGameFlow>>()
            .ToList();

        Action<IGameFlow>? ITurn.ChosenAction
            => ChosenAction as Action<IGameFlow>;

        IGameFlow ITurn.Game
            => (IGameFlow)Game;

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