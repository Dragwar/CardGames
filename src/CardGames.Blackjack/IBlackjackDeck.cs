using CardGames.Shared.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CardGames.Blackjack
{
    public interface IBlackjackDeck : IDeck, IEnumerable<IBlackjackCard>
    {
        //IReadOnlyList<IBlackjackCard> Cards { get; }

        //IBlackjackCard Deal([AllowNull] int? cardIndex = null);

        //IEnumerable<IBlackjackCard> DealAll();

        //IBlackjackCard? DealOrDefault([AllowNull] int? cardIndex = null);

        //void FillDeck(bool isShuffled = true);

        //IBlackjackCard? Peek([AllowNull] int? cardIndex = null);

        //void Shuffle();
    }
}