using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Shared.Models
{
    public abstract class GameFlowBase : IGameFlow
    {
        protected readonly IList<IPlayer> _players;

        public IReadOnlyList<IPlayer> Players
            => new ReadOnlyCollection<IPlayer>(_players);

        public IDeck Deck { get; }

        public string Name { get; }

        public IList<ITurn> TurnHistory { get; }
        public IList<ITurn> TurnOrder { get; }

        public ITurn? CurrentTurn { get; protected set; }

        protected GameFlowBase(string name, IDeck deck, IList<IPlayer> players)
        {
            Name = name;
            Deck = deck;
            _players = players;
            TurnHistory = new List<ITurn>(25);
            TurnOrder = new List<ITurn>(_players.Count); //TODO: figure out how to set TurnOrder
        }

        public virtual void SetTurnOrder<TKey>(Func<IPlayer, TKey> reorderFunction)
        {
            var tempList = new List<IPlayer>(_players.OrderBy(reorderFunction));
            _players.Clear();
            tempList.ForEach(_players.Add);
            //TODO: figure out how to set TurnOrder
        }

        public void Deal(IPlayer player)
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