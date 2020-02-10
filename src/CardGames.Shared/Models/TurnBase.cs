using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    public abstract class TurnBase : ITurn
    {
        public abstract IList<Action<IGameFlow>> AvailableActions { get; protected set; }

        public IGameFlow Game { get; }

        public IPlayer Player { get; }
        public abstract Action<IGameFlow>? ChosenAction { get; }

        protected TurnBase(IGameFlow game, IPlayer player)
        {
            Game = game;
            Player = player;
        }
    }
}