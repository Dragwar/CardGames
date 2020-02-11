using CardGames.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackTurn : ITurn, IBlackjackTurn
    {
        public IList<IBlackjackTurnAction> AvailableActions { get; }

        public IBlackjackTurnAction? ChosenAction { get; set; }

        public IBlackjackGameFlow Game { get; }

        public IBlackjackPlayer Player { get; }

        IPlayer ITurn.Player
            => (IPlayer)Player;

        IList<ITurnAction> ITurn.AvailableActions
            => AvailableActions
            .Cast<ITurnAction>()
            .ToList();

        ITurnAction? ITurn.ChosenAction
            => ChosenAction as ITurnAction;

        IGameFlow ITurn.Game
            => (IGameFlow)Game;

        public BlackjackTurn(
            IBlackjackGameFlow game,
            IBlackjackPlayer player,
            IList<IBlackjackTurnAction> availableActions)
        {
            Game = game;
            Player = player;
            AvailableActions = availableActions;
        }
    }
}