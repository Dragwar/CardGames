using CardGames.Shared.Models;

namespace CardGames.Blackjack
{
    public interface IBlackjackHand : IHand<IBlackjackCard>
    {
        bool IsBust { get; }
        bool IsBlackJack { get; }
    }
}