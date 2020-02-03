using System.Collections.Generic;

namespace CardGames.Shared.Services
{
    /// <summary>
    /// Represents a simple service that can shuffle the order of an <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    public interface IShuffleService<T>
    {
        /// <summary>
        /// Shuffles the order of the <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The items to be shuffled.</param>
        IEnumerable<T> Shuffle(IEnumerable<T> items);
    }
}