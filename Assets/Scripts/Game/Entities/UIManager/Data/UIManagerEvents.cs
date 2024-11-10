namespace Game.Entities.UITextElement.Data
{
    public class OnUIManagerUpdateEvent
    {
        public bool ResetTime { get; set; }
        public int Turns { get; set; }
        public int Combo { get; set; }
        public int Score { get; set; }
    }
}