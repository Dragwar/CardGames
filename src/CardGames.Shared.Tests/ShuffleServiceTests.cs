using CardGames.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGames.Shared.Tests
{
    public class ShuffleServiceTests
    {
        #region MemberData

        public static IEnumerable<object[]> Get_Shuffle_Theory_Data()
            => new List<object[]>
            {
                new object[] { Array.Empty<int>() },
                new object[] { Enumerable.Repeat(5, 5).ToArray() },
                new object[] { Enumerable.Range(1, 10).ToArray() },
                new object[] { Enumerable.Range(-50, 3000).ToArray() },
            };

        public static IEnumerable<object[]> Get_Shuffle_Theory_Data(int skip)
            => Get_Shuffle_Theory_Data().Skip(skip);

        public static IEnumerable<object[]> Get_Shuffle_Theory_Data(int skip, int take)
            => Get_Shuffle_Theory_Data().Skip(skip).Take(take);

        #endregion MemberData

        [Fact]
        public void Implments_IShuffleServiceInterface_Fact()
        {
            var shuffleService = new ShuffleService<object>();

            Assert.IsAssignableFrom<IShuffleService<object>>(shuffleService);
        }

        [Theory, MemberData(nameof(Get_Shuffle_Theory_Data))]
        public void Shuffle_ShouldHaveSameLength_Theory(int[] items)
        {
            var shuffleService = new ShuffleService<int>();

            var shuffledItems = shuffleService
                .Shuffle(items)
                .ToArray();

            Assert.Equal(items.Length, shuffledItems.Length);
        }

        [Theory, MemberData(nameof(Get_Shuffle_Theory_Data), 2)]
        public void Shuffle_ShouldNotEqual_Theory(int[] items)
        {
            var shuffleService = new ShuffleService<int>();
            var shuffledItems = shuffleService
                .Shuffle(items)
                .ToArray();

            Assert.NotEqual(items, shuffledItems);
        }

        [Theory, MemberData(nameof(Get_Shuffle_Theory_Data), 0, 2)]
        public void Shuffle_ShouldEqual_Theory(int[] items)
        {
            var shuffleService = new ShuffleService<int>();

            var shuffledItems = shuffleService
                .Shuffle(items)
                .ToArray();

            Assert.Equal(items, shuffledItems);
        }
    }
}