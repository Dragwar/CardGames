using CardGames.Shared;
using CardGames.Shared.Services;
using CardGames.Shared.Services.Extensions;
using System;
using System.Linq;
using System.Text;

namespace CardGames.Blackjack.ConsoleApplication
{
    public sealed class Program
    {
        public static string[] _args = Array.Empty<string>();

        public static void Main(string[] args)
        {
            _args = args;

            var playerNames = Constants.PlayerNames._male
                .Concat(Constants.PlayerNames._female)
                .Shuffle();

            var players = playerNames
                .Select(name => new BlackjackPlayer(name, new BlackjackHand()))
                .Cast<IBlackjackPlayer>()
                .Take(3)
                .ToList();

            var deck = new BlackjackDeck(new ShuffleService<IBlackjackCard>());
            deck.FillDeck();
            var game = new BlackjackGameFlow("Game1", deck, players);

            Console.WriteLine("Deck:\n");
            var sb = new StringBuilder(50);
            for (var i = 1; i <= game.Deck.Cards.Count; i++)
            {
                var card = game.Deck.Cards[i - 1];
                sb.AppendFormat(" <{0,2}> -~- ", i)
                    .AppendFormat("[{0,2}] ", card.Value)
                    .AppendFormat("{0,5} -- {1,5}", card.Name, card.Suit)
                    .AppendLine();
            }
            Console.WriteLine(sb.ToString());
            sb.Clear();

            Console.WriteLine("\nPlayers:\n");
            for (var i = 1; i <= game.Players.Count; i++)
            {
                var player = game.Players[i - 1];
                sb.AppendFormat(" <{0,2}> -~- ", i)
                    .AppendLine(player.Name);
            }
            Console.WriteLine(sb.ToString());
            sb.Clear();

            Console.WriteLine("\nDealing cards to players:\n");
            for (int i = 1; i <= game.Players.Count; i++)
            {
                var player = game.Players[i - 1];
                sb.AppendFormat(" <{0,2}> -~- {1}", i, player.Name)
                    .AppendLine();
                var hand = new IBlackjackCard[2];
                for (int j = 1; j <= 2; j++)
                {
                    var dealtCard = game.Deck.Deal();
                    sb.AppendFormat("\t({0}): ", j)
                        .AppendFormat("{0}", CardString(dealtCard))
                        .AppendLine();
                    hand[j - 1] = dealtCard;
                }
                var handValue = hand.Sum(c => c.Value);
                sb.AppendFormat("\tHand:\n\t\tValue: {0}", handValue)
                    .AppendLine()
                    .AppendFormat("\t\tIsBlackjack: {0}", handValue == 21)
                    .AppendLine();
            }
            Console.WriteLine(sb.ToString());
            sb.Clear();

            Console.ReadLine();

            static string CardString(IBlackjackCard card)
                => string.Format("[{0,2}] {1,5} -- {2,5}", card.Value, card.Name, card.Suit);
        }
    }
}