using System;

namespace CardGames.Shared.Models
{
    public class TurnAction : ITurnAction
    {
        public enum State
        {
            NotStarted,
            Started,
            Ended,
            Failed
        }

        private readonly Action<IGameFlow> _action;
        private readonly Func<IGameFlow, bool>? _canExcuteAction;

        public string ActionName { get; }

        public State CurrentState { get; private set; }

        public TurnAction(string actionName, Action<IGameFlow> action, Func<IGameFlow, bool>? canExcuteAction)
        {
            ActionName = actionName;
            _action = action;
            _canExcuteAction = canExcuteAction;
            _action ??= fakeMethod;
            CurrentState = State.NotStarted;

            static void fakeMethod(IGameFlow _) { }
        }

        public void Excute(IGameFlow game)
        {
            CurrentState = State.Started;
            _action.Invoke(game);
            CurrentState = State.Ended;
        }

        public bool CanExcute(IGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;
    }
}