using CardGames.Shared.Models;

namespace CardGames.Blackjack
{
    public class BlackjackPlayer : IBlackjackPlayer
    {
        public string Name { get; set; }
        public IBlackjackHand Hand { get; set; }

        IHand<IBlackjackCard> IPlayer<IBlackjackCard>.Hand
        {
            get => Hand;
            set => Hand = value as IBlackjackHand ?? Hand;
        }

        public BlackjackPlayer(string name, IBlackjackHand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}