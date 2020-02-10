namespace CardGames.Blackjack
{
    public interface IBlackjackPlayer
    {
        IBlackjackHand Hand { get; set; }
        string Name { get; set; }
    }
}