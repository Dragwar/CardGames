using CardGames.Shared.Models;

namespace CardGames.Blackjack
{
    public interface IBlackjackTurnAction : ITurnAction
    {
        bool CanExcute(IBlackjackGameFlow game);
        void Excute(IBlackjackGameFlow game);
    }
}