namespace CardGames.Shared.Models
{
    public class Player : IPlayer<ICard>
    {
        public string Name { get; set; }
        public IHand<ICard> Hand { get; set; }

        public Player(string name, IHand<ICard> hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}