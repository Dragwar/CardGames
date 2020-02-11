namespace CardGames.Shared.Models
{
    public interface ITurnAction
    {
        string ActionName { get; }

        void Excute(IGameFlow game);
    }
}