using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    public abstract class TurnBase : ITurn
    {
        public abstract IList<ITurnAction> AvailableActions { get; protected set; }

        public IGameFlow Game { get; }

        public IPlayer Player { get; }
        public abstract ITurnAction? ChosenAction { get; }

        protected TurnBase(IGameFlow game, IPlayer player)
        {
            Game = game;
            Player = player;
        }
    }
}