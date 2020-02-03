namespace CardGames.Shared.Models
{
    public class Card : ICard
    {
        public Suit Suit { get; }
        public CardNameValue Name { get; }
        public int Value => (int)Name;

        public Card(CardNameValue name, Suit suit)
        {
            Name = name;
            Suit = suit;
        }
    }
}