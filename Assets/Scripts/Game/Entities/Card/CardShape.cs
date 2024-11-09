using System;

namespace Game.Entities.Card
{
    public enum CardShape
    {
        Spade = 0,
        Heart = 1,
        Diamond = 2,
        Club = 3,
        Back = 4,
        None = 5
    }
    
    public static class CardShapeExtension
    {
        public static string GetString(this CardShape cardShape)
        {
            return cardShape switch
            {
                CardShape.Spade => "spades",
                CardShape.Heart => "hearts",
                CardShape.Diamond => "diamonds",
                CardShape.Club => "clubs",
                CardShape.Back => "back01",
                _ => throw new Exception("Invalid card shape")
            };
        }
    }
}