using System.Collections.Generic;
using System.Linq;

namespace CardGames.Shared.Services.Extensions
{
    public static class ShuffleExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            return items.OrderBy(DiscardForRandomNumber);
            static int DiscardForRandomNumber(T _)
                => RNG.Next();
        }
    }
}