using CardGames.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackGameFlow : IGameFlow, IBlackjackGameFlow
    {
        private readonly IList<IBlackjackPlayer> _players;

        public IBlackjackTurn? CurrentTurn { get; set; }

        public IBlackjackDeck Deck { get; }

        public string Name { get; }

        public IReadOnlyList<IBlackjackPlayer> Players
            => new ReadOnlyCollection<IBlackjackPlayer>(_players);

        public IList<IBlackjackTurn> TurnHistory { get; }
        public IList<IBlackjackTurn> TurnOrder { get; }

        IList<ITurn> IGameFlow.TurnOrder
            => TurnOrder
            .Cast<ITurn>()
            .ToList();

        ITurn? IGameFlow.CurrentTurn
            => CurrentTurn as ITurn;

        IDeck IGameFlow.Deck
            => (IDeck)Deck;

        IReadOnlyList<IPlayer> IGameFlow.Players
            => Players
            .Cast<IPlayer>()
            .ToList();

        IList<ITurn> IGameFlow.TurnHistory
            => TurnHistory
            .Cast<ITurn>()
            .ToList();

        public BlackjackGameFlow(string name, IBlackjackDeck deck, IList<IBlackjackPlayer> players)
        {
            Name = name;
            Deck = deck;
            _players = players;
            TurnHistory = new List<IBlackjackTurn>(25);
            TurnOrder = new List<IBlackjackTurn>(_players.Select(p => new BlackjackTurn(this, p)));
        }

        public void SetTurnOrder<TKey>(Func<IBlackjackPlayer, TKey> reorderFunction)
        {
            var tempPlayerList = new List<IBlackjackPlayer>(_players.OrderBy(reorderFunction));
            _players.Clear();
            tempPlayerList.ForEach(_players.Add);

            var tempTurnOrderList = new List<IBlackjackTurn>(TurnOrder);
            TurnOrder.Clear();
            foreach (var player in tempPlayerList)
            {
                for (int i = 0; i < TurnOrder.Count; i++)
                {
                    var turn = tempTurnOrderList[i];
                    if (player == turn.Player)
                    {
                        TurnOrder[i] = turn;
                    }
                }
            }
        }

        public void Deal(IBlackjackPlayer player)
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

        public void SetTurnOrder<TKey>(Func<IPlayer, TKey> reorderFunction)
            => SetTurnOrder((Func<IBlackjackPlayer, TKey>)reorderFunction);

        public void Deal(IPlayer player)
            => Deal((IBlackjackPlayer)player);
    }
}