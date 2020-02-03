using System;
using System.Collections.Generic;

namespace CardGames.Shared.Models
{
    public abstract class TurnBase<TCard> : ITurn<TCard>
        where TCard : class, ICard
    {
        public abstract IList<Action<IGameFlow<TCard>>> AvailableActions { get; protected set; }

        public IGameFlow<TCard> Game { get; }

        public IPlayer<TCard> Player { get; }
        public abstract Action<IGameFlow<TCard>>? ChosenAction { get; }

        protected TurnBase(IGameFlow<TCard> game, IPlayer<TCard> player)
        {
            Game = game;
            Player = player;
        }

        public abstract void StartTurn();

        public abstract void EndTurn();
    }
}