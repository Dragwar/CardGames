using CardGames.Shared.Models;
using CardGames.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGames.Shared.Tests
{
    public class DeckTests
    {
        #region MemberData

        public static IEnumerable<object[]> Get_Random_Cards_Theroy_Data()
            => new List<object[]>
            {
                new object[]
                {
                    Enumerable
                        .Range(1, RNG.Next(1, 53))
                        .Select(cardIndex => new Card((CardNameValue)RNG.Next(0, 14), (Suit)RNG.Next(0, 5)))
                        .ToArray(),
                },
            };

        public static IEnumerable<object[]> Get_Random_Cards_Theroy_Data(int generateMin)
            => new List<object[]>
            {
                new object[]
                {
                    Enumerable
                        .Range(1, RNG.Next(generateMin, 53))
                        .Select(cardIndex => new Card((CardNameValue)RNG.Next(0, 14), (Suit)RNG.Next(0, 5)))
                        .ToArray(),
                },
            };

        #endregion MemberData

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Cards_NoShuffleShouldHaveSameLength_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);

            Assert.Equal(items.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_NoShuffleShouldEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);

            Assert.Equal(items, deck.Cards.ToArray());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_ShuffleShouldHaveSameLength_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);

            Assert.Equal(items.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_ShuffleShouldNotEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);

            Assert.NotEqual(items, deck.Cards.ToArray());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_NoShuffleShouldRemoveOneCard_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);
            var expected = deck.Deal();
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_ShuffleShouldRemoveOneCard_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);
            var expected = deck.Deal();
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 3)]
        public void Deal_NoShuffleAtIndexShouldRemoveOneCard_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);
            var expected = deck.Deal(3);
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 3)]
        public void Deal_ShuffleAtIndexShouldRemoveOneCard_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);
            var expected = deck.Deal(3);
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_NoShuffleShouldEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);
            var expected = items[0];
            Assert.Equal(expected, deck.Deal());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_NoShuffleAtIndexShouldEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);
            var index = items.Length - 1;
            var expected = items[index];
            Assert.Equal(expected, deck.Deal(index));
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_ShuffleShouldNotEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);
            var expected = items[0];
            Assert.NotEqual(expected, deck.Deal());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_ShuffleAtIndexShouldNotEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);
            var index = items.Length - 1;
            var expected = items[index];
            Assert.NotEqual(expected, deck.Deal(index));
        }
    }
}