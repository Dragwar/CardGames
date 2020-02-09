using CardGames.Shared.Services.Extensions;
using System.Collections.Generic;

namespace CardGames.Shared.Services
{
    public class ShuffleService<T> : IShuffleService<T>
    {
        public IEnumerable<T> Shuffle(IEnumerable<T> items)
            => items.Shuffle();
    }
}