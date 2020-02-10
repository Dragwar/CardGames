namespace CardGames.Shared.Models
{
    /// <summary>
    /// Represents a player of a <see cref="ICard"/> game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// player's hand of cards.
        /// </summary>
        public IHand Hand { get; set; }
    }
}