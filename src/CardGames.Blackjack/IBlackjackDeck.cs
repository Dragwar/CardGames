using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CardGames.Blackjack
{
    public interface IBlackjackDeck
    {
        IReadOnlyList<IBlackjackCard> Cards { get; }

        IBlackjackCard Deal([AllowNull] int? cardIndex = null);
        IEnumerable<IBlackjackCard> DealAll();
        IBlackjackCard? DealOrDefault([AllowNull] int? cardIndex = null);
        void FillDeck(bool isShuffled = true);
        IEnumerator<IBlackjackCard> GetEnumerator();
        IBlackjackCard? Peek([AllowNull] int? cardIndex = null);
        void Shuffle();
    }
}