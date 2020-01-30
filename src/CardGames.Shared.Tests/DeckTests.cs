using CardGames.Shared.Models;
using CardGames.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGames.Shared.Tests
{
    public class DeckTests
    {
        #region MemberData

        public static IEnumerable<object[]> Get_Default_Cards_Theory_Data()
            => new List<object[]>
            {
                new object[]
                {
                    Array.Empty<Card>(),
                },
                new object[]
                {
                    new Card[]
                    {
                        new Card(CardNameValue.Ace, Suit.Spades),
                    }
                },
                new object[]
                {
                    new Card[]
                    {
                        new Card(CardNameValue.Five, Suit.Spades),
                        new Card(CardNameValue.Eight, Suit.Hearts),
                        new Card(CardNameValue.King, Suit.Clubs),
                        new Card(CardNameValue.Jack, Suit.Clubs),
                    },
                },
                new object[]
                {
                    new Card[]
                    {
                        new Card(CardNameValue.Three, Suit.Diamonds),
                        new Card(CardNameValue.Ace, Suit.Clubs),
                        new Card(CardNameValue.Two, Suit.Hearts),
                        new Card(CardNameValue.Queen, Suit.Spades),
                    },
                },
                new object[]
                {
                    new Card[]
                    {
                        new Card(CardNameValue.Ace, Suit.Clubs),
                        new Card(CardNameValue.Ace, Suit.Diamonds),
                        new Card(CardNameValue.Ace, Suit.Hearts),
                        new Card(CardNameValue.Ace, Suit.Spades),
                    },
                },
                new object[]
                {
                    new Card[]
                    {
                        new Card(CardNameValue.Seven, Suit.Clubs),
                        new Card(CardNameValue.Six, Suit.Clubs),
                        new Card(CardNameValue.Four, Suit.Clubs),
                        new Card(CardNameValue.Ten, Suit.Clubs),
                    },
                },
            };

        public static IEnumerable<object[]> Get_Default_Cards_Theory_Data(int skip)
            => Get_Default_Cards_Theory_Data().Skip(skip);

        public static IEnumerable<object[]> Get_Default_Cards_Theory_Data(int skip, int take)
            => Get_Default_Cards_Theory_Data().Skip(skip).Take(take);

        #endregion MemberData

        [Theory, MemberData(nameof(Get_Default_Cards_Theory_Data))]
        public void Cards_NoShuffleShouldHaveSameLength_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);

            Assert.Equal(items.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Default_Cards_Theory_Data))]
        public void Cards_NoShuffleShouldEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: false);

            Assert.Equal(items, deck.Cards.ToArray());
        }

        [Theory, MemberData(nameof(Get_Default_Cards_Theory_Data))]
        public void Cards_ShuffleShouldHaveSameLength_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);

            Assert.Equal(items.Length, deck.Cards.Count);
        }

        [Theory, MemberData(nameof(Get_Default_Cards_Theory_Data), 2)]
        public void Cards_ShuffleShouldEqual_Theory(Card[] items)
        {
            var deck = new Deck(new ShuffleService<Card>(), initialCards: items, isShuffled: true);

            Assert.NotEqual(items, deck.Cards.ToArray());
        }
    }
}