using CardGames.Shared;
using System.Diagnostics;

namespace CardGames.Blackjack
{
    [DebuggerDisplay("[{" + nameof(Value) + "}] {" + nameof(Name) + "} -- {" + nameof(Suit) + "}")]
    public class BlackjackCard : IBlackjackCard
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