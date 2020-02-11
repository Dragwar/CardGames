using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public class BlackjackTurnAction : ITurnAction, IBlackjackTurnAction
    {
        public enum State
        {
            NotStarted,
            Started,
            Ended,
            Failed
        }

        public static IReadOnlyList<IBlackjackTurnAction> AllBlackjackTurnActions
            => new List<IBlackjackTurnAction>
            {
                new BlackjackTurnAction(
                    "Hit Me",
                    g => g.Deal(g.CurrentTurn!.Player),
                    g => !g.Deck.IsEmpty && g.CurrentTurn is { },
                    (sender, e) => 
                    {
                        if (sender is IBlackjackTurnAction turnAction)
                        {
                            turnAction.
	                    }
                    }),
            };

        private readonly Action<IBlackjackGameFlow> _action;
        private readonly Func<IGameFlow, bool>? _canExcuteAction;

        public string ActionName { get; }
        public State CurrentState { get; private set; }

        public event EventHandler<State> OnStateChanged = delegate { };

        public BlackjackTurnAction(string actionName, Action<IBlackjackGameFlow> action, Func<IGameFlow, bool>? canExcuteAction, EventHandler<State>? stateChangedListener)
        {
            ActionName = actionName;
            _action = action;
            _canExcuteAction = canExcuteAction;
            _action ??= delegate { };
            CurrentState = State.NotStarted;
            OnStateChanged += stateChangedListener ?? delegate { };
        }

        public void Excute(IBlackjackGameFlow game)
        {
            CurrentState = State.Started;
            _action.Invoke(game);
            CurrentState = State.Ended;
        }

        public void Excute(IGameFlow game)
            => Excute((IBlackjackGameFlow)game);

        public bool CanExcute(IBlackjackGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;

        public bool CanExcute(IGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;
    }
}