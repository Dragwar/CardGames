namespace CardGames.Shared.Models
{
    public interface ITurnAction
    {
        string ActionName { get; }

        TurnActionState State { get; }

        bool CanExcute(IGameFlow game);

        void Excute(IGameFlow game);
    }
}