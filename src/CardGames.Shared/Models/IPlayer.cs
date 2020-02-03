namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a player of a <typeparamref name="TCard"/> game.
    /// </summary>
    public interface IPlayer<TCard>
        where TCard : class, ICard
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// player's hand of cards.
        /// </summary>
        public IHand<TCard> Hand { get; set; }
    }
}