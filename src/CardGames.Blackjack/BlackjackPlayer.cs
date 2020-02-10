using CardGames.Shared.Models;
using System.Diagnostics;

namespace CardGames.Blackjack
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class BlackjackPlayer : IBlackjackPlayer
    {
        public string Name { get; set; }
        public IBlackjackHand Hand { get; set; }

        IHand IPlayer.Hand
        {
            get => (IHand)Hand;
            set => Hand = value as IBlackjackHand ?? Hand;
        }

        public BlackjackPlayer(string name, IBlackjackHand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}