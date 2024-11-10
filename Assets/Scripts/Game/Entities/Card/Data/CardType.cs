using System;

namespace Game.Entities.Card.Data
{
    public enum CardType
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
        None = 15
    }

    public static class CardTypeExtension
    {
        public static string GetString(this CardType cardType)
        {
            return cardType switch
            {
                CardType.Two => "_02",
                CardType.Three => "_03",
                CardType.Four => "_04",
                CardType.Five => "_05",
                CardType.Six => "_06",
                CardType.Seven => "_07",
                CardType.Eight => "_08",
                CardType.Nine => "_09",
                CardType.Ten => "_10",
                CardType.Jack => "_jack",
                CardType.Queen => "_queen",
                CardType.King => "_king",
                CardType.Ace => "_ace",
                CardType.None => "",
                _ => throw new Exception("Invalid card type")
            };
        }
    }
}