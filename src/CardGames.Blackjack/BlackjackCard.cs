using CardGames.Shared;

namespace CardGames.Blackjack
{
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