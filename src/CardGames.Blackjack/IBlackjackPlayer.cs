using CardGames.Shared.Models;

namespace CardGames.Blackjack
{
    public interface IBlackjackPlayer : IPlayer
    {
        new IBlackjackHand Hand { get; set; }
        //string Name { get; set; }
    }
}