using CardGames.Shared;
using CardGames.Shared.Models;
using System.Diagnostics;

namespace CardGames.Blackjack
{
    [DebuggerDisplay("[{" + nameof(Value) + "}] {" + nameof(Name) + "} -- {" + nameof(Suit) + "}")]
    public class BlackjackCard : ICard, IBlackjackCard
    {
        public CardNameValue Name { get; }

        public Suit Suit { get; }

        public int Value
            => (int)Name;

        public BlackjackCard(CardNameValue name, Suit suit)
        {
            Name = name;
            Suit = suit;
        }
    }
}