using CardGames.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGames.Blackjack
{
    public class BlackjackGameFlow : IBlackjackGameFlow
    {
        private readonly IList<IBlackjackPlayer> _players;

        public ITurn<IBlackjackCard>? CurrentTurn { get; set; }

        public IBlackjackDeck Deck { get; }

        public string Name { get; }

        public IReadOnlyList<IBlackjackPlayer> Players
            => new ReadOnlyCollection<IBlackjackPlayer>(_players);

        public IList<IBlackjackTurn> TurnHistory { get; }

        IDeck<IBlackjackCard> IGameFlow<IBlackjackCard>.Deck
            => Deck;

        IReadOnlyList<IPlayer<IBlackjackCard>> IGameFlow<IBlackjackCard>.Players
            => Players;

        IList<ITurn<IBlackjackCard>> IGameFlow<IBlackjackCard>.TurnHistory
            => (IList<ITurn<IBlackjackCard>>)TurnHistory;

        public BlackjackGameFlow(string name, IBlackjackDeck deck, IList<IBlackjackPlayer> players)
        {
            Name = name;
            Deck = deck;
            _players = players;
            TurnHistory = new List<IBlackjackTurn>(25);
        }

        public void SetTurnOrder<TKey>(Func<IBlackjackPlayer, TKey> reorderFunction)
        {
            var tempList = new List<IBlackjackPlayer>(_players.OrderBy(reorderFunction));
            _players.Clear();
            tempList.ForEach(_players.Add);
        }

        void IGameFlow<IBlackjackCard>.SetTurnOrder<TKey>(Func<IPlayer<IBlackjackCard>, TKey> reorderFunction)
            => SetTurnOrder(reorderFunction);
    }
}