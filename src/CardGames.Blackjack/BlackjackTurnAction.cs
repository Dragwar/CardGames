using CardGames.Shared.Models;
using System;
using System.Collections.Generic;

namespace CardGames.Blackjack
{
    public class BlackjackTurnAction : ITurnAction, IBlackjackTurnAction
    {
        public static IReadOnlyList<IBlackjackTurnAction> AllBlackjackTurnActions
            => new List<IBlackjackTurnAction>
            {
                new BlackjackTurnAction(
                    "Hit Me",
                    g => g.Deal(g.CurrentTurn!.Player),
                    g => !g.Deck.IsEmpty && g.CurrentTurn is { }),
            };

        private readonly Action<IBlackjackGameFlow> _action;
        private readonly Func<IGameFlow, bool>? _canExcuteAction;

        public string ActionName { get; }
        public TurnActionState State { get; private set; }


        public BlackjackTurnAction(string actionName, Action<IBlackjackGameFlow> action, Func<IGameFlow, bool>? canExcuteAction)
        {
            ActionName = actionName;
            _action = action;
            _canExcuteAction = canExcuteAction;
            _action ??= delegate { };
            State = TurnActionState.NotStarted;
        }

        public void Excute(IBlackjackGameFlow game)
        {
            State = TurnActionState.Started;
            _action.Invoke(game);
            State = TurnActionState.Ended;
        }

        public void Excute(IGameFlow game)
            => Excute((IBlackjackGameFlow)game);

        public bool CanExcute(IBlackjackGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;

        public bool CanExcute(IGameFlow game)
            => _canExcuteAction?.Invoke(game) ?? true;
    }
}