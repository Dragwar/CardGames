using System.Collections.Generic;
using System.Linq;

namespace CardGames.Shared.Services
{
    public class ShuffleService<T> : IShuffleService<T>
    {
        public IEnumerable<T> Shuffle(IEnumerable<T> items)
        {
            return items.OrderBy(DiscardForRandomNumber);
            static int DiscardForRandomNumber(T _)
                => RNG.Next();
        }
    }
}