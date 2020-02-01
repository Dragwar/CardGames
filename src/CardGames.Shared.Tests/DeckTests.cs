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
            => Get_Random_Cards_Theroy_Data(1);

        public static IEnumerable<object[]> Get_Random_Cards_Theroy_Data(int generateMin)
            => new List<object[]>
            {
                new object[]
                {
                    Enumerable
                        .Range(1, RNG.Next(generateMin, 53))
                        .Select(_ => new Card((CardNameValue)RNG.Next(1, 14), (Suit)RNG.Next(1, 5)))
                        .ToArray(),
                },
            };

        #endregion MemberData

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Cards_NoShuffleShouldHaveSameLength_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);

            Assert.Equal(cards.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_NoShuffleShouldEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);

            Assert.Equal(cards, deck.Cards.ToArray());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_ShuffleShouldHaveSameLength_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);

            Assert.Equal(cards.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Cards_ShuffleShouldNotEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);

            Assert.NotEqual(cards, deck.Cards.ToArray());
        }

        #region Deal Methods

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_NoShuffleShouldRemoveOneCard_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);
            var expected = deck.Deal();
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_ShuffleShouldRemoveOneCard_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);
            var expected = deck.Deal();
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 3)]
        public void Deal_NoShuffleAtIndexShouldRemoveOneCard_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);
            var expected = deck.Deal(3);
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 3)]
        public void Deal_ShuffleAtIndexShouldRemoveOneCard_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);
            var expected = deck.Deal(3);
            Assert.DoesNotContain(expected, deck.Cards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_NoShuffleShouldEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);
            var expected = cards[0];
            Assert.Equal(expected, deck.Deal());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 5)]
        public void Deal_NoShuffleAtIndexShouldEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);
            var index = cards.Length - 1;
            var expected = cards[index];
            Assert.Equal(expected, deck.Deal(index));
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_ShuffleShouldNotEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);
            var expected = cards[0];
            Assert.NotEqual(expected, deck.Deal());
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void Deal_ShuffleAtIndexShouldNotEqual_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);
            var index = cards.Length - 1;
            var expected = cards[index];
            Assert.NotEqual(expected, deck.Deal(index));
        }

        #endregion Deal Methods

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 1)]
        public void DealAll_NoShuffleShouldRemoveAllCards_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: false);
            var dealtCards = deck.DealAll().ToArray();
            Assert.Empty(deck.Cards);
            Assert.NotEmpty(dealtCards);
            Assert.Equal(cards, dealtCards);
        }

        [Theory, MemberData(nameof(Get_Random_Cards_Theroy_Data), 25)]
        public void DealAll_ShuffleShouldRemoveAllCards_Theory(Card[] cards)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: cards, isShuffled: true);
            var dealtCards = deck.DealAll().ToArray();
            Assert.Empty(deck.Cards);
            Assert.NotEmpty(dealtCards);
            Assert.NotEqual(cards, dealtCards);
        }
    }
}