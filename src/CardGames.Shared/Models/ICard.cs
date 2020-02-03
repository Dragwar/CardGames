namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a playing card.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Name of the card.
        /// </summary>
        CardNameValue Name { get; }

        /// <summary>
        /// Suit of the card.
        /// </summary>
        Suit Suit { get; }

        /// <summary>
        /// Numeric value of card.
        /// </summary>
        int Value { get; }
    }
}