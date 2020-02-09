using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Shared.Models
{
    public abstract class GameFlowBase<TCard> : IGameFlow<TCard>
        where TCard : class, ICard
    {
        protected readonly IList<IPlayer<TCard>> _players;

        public IReadOnlyList<IPlayer<TCard>> Players
            => new ReadOnlyCollection<IPlayer<TCard>>(_players);

        public IDeck<TCard> Deck { get; }

        public string Name { get; }

        public IList<ITurn<TCard>> TurnHistory { get; }

        public ITurn<TCard>? CurrentTurn { get; protected set; }

        protected GameFlowBase(string name, IDeck<TCard> deck, IList<IPlayer<TCard>> players)
        {
            Name = name;
            Deck = deck;
            _players = players;
            TurnHistory = new List<ITurn<TCard>>(25);
        }

        public virtual void SetTurnOrder<TKey>(Func<IPlayer<TCard>, TKey> reorderFunction)
        {
            var tempList = new List<IPlayer<TCard>>(_players.OrderBy(reorderFunction));
            _players.Clear();
            tempList.ForEach(_players.Add);
        }

        public void Deal(IPlayer<TCard> player)
        {
            if (!_players.Contains(player))
            {
                throw new ArgumentException(
                    nameof(player),
                    $"Player not found in this game! {{{player.Name}}}");
            }

            if (Deck.DealOrDefault() is { } dealtCard)
            {
                player.Hand.Add(dealtCard);
            }
            else
            {
                throw new InvalidOperationException($"The {nameof(Deck)} has no cards left to deal!");
            }
        }
    }
}