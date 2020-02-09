using System;
using System.Runtime.Serialization;

namespace CardGames.Blackjack.Exceptions
{
    [Serializable]
    public class BlackjackHandSizeTooLargeException : Exception
    {
        private const string HAND_CANT_BE_GREATER_LARGER_THAN_14_CARDS = "Hand can't be greater larger than 14 cards";

        public BlackjackHand? Hand { get; }

        public BlackjackHandSizeTooLargeException()
        {
        }

        public BlackjackHandSizeTooLargeException(string message = HAND_CANT_BE_GREATER_LARGER_THAN_14_CARDS)
            : base(message)
        {
        }

        public BlackjackHandSizeTooLargeException(BlackjackHand hand, string message = HAND_CANT_BE_GREATER_LARGER_THAN_14_CARDS)
            : base(message)
        {
            Hand = hand;
        }

        public BlackjackHandSizeTooLargeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BlackjackHandSizeTooLargeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}