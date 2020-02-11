using CardGames.Shared.Models;

namespace CardGames.Blackjack
{
    public interface IBlackjackTurnAction : ITurnAction
    {
        void Excute(IBlackjackGameFlow game);
    }
}