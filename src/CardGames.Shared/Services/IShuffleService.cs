using System.Collections.Generic;

namespace CardGames.Shared.Services
{
    public interface IShuffleService<T>
    {
        IEnumerable<T> Shuffle(IEnumerable<T> items);
    }
}