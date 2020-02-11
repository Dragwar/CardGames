using CardGames.Shared.Models;
using System;

namespace CardGames.Blackjack
{
    public class BlackjackTurnAction : IBlackjackTurnAction
    {
        public enum State
        {
            NotStarted,
            Started,
            Ended,
            Failed
        }

        private readonly Action<IBlackjackGameFlow> _action;

        public string ActionName { get; }
        public State CurrentState { get; private set; }

        public BlackjackTurnAction(string actionName, Action<IBlackjackGameFlow> action)
        {
            ActionName = actionName;
            _action = action;
            _action ??= fakeMethod;
            CurrentState = State.NotStarted;

            static void fakeMethod(IBlackjackGameFlow _) { }
        }

        public void Excute(IBlackjackGameFlow game)
        {
            CurrentState = State.Started;
            _action.Invoke(game);
            CurrentState = State.Ended;
        }

        public void Excute(IGameFlow game)
            => Excute((IBlackjackGameFlow)game);
    }
}