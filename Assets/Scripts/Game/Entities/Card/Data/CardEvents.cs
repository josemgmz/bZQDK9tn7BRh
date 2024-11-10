using UnityEngine.Events;

namespace Game.Entities.Card.Data
{
    public class OnCardSetupEvent
    {
        public CardShape CardShape { get; set; }
        public CardType CardType { get; set; }
    }
    
    public class OnCardFlippedEvent : OnCardSetupEvent
    {
        public UnityAction OnSuccess { get; set; }
        public UnityAction OnFail { get; set; }
    }
    
    public class OnCardFlipEvent {}
}