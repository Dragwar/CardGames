using System;

namespace CardGames.Shared.Models
{
    public class TurnAction : ITurnAction
    {
        private readonly Action<IGameFlow> _action;
        private readonly Func<IGameFlow, bool>? _canExcuteAction;

        public string ActionName { get; }

        public TurnActionState State { get; private set; }

        public TurnAction(string actionName, Action<IGameFlow> action, Func<IGameFlow, bool>? canExcuteAction)
        {
            ActionName = actionName;
            _action = action;
            _canExcuteAction = canExcuteAction;
            _action ??= delegate { };
            State = TurnActionState.NotStarted;
        }

        public void Excute(IGameFlow game)
        {
            State = TurnActionState.Started;
            _action.Invoke(game);
            State = TurnActionState.Ended;
        }

        public bool CanExcute(IGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;
    }
}