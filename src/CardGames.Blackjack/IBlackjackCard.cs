using CardGames.Shared;

namespace CardGames.Blackjack
{
    public interface IBlackjackCard
    {
        CardNameValue Name { get; }
        Suit Suit { get; }
        int Value { get; }
    }
}