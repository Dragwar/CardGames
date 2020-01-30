namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a playing card.
    /// </summary>
    public class Card
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