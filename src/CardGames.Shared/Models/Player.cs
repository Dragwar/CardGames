namespace CardGames.Shared.Models
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public IHand Hand { get; set; }

        public Player(string name, IHand hand)
        {
            Name = name;
            Hand = hand;
        }
    }
}